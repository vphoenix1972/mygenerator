using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Utils.Factories;
using TemplateProject.Utils.Md5;
using TemplateProject.Web.Configuration;
using TemplateProject.Web.Security;

namespace TemplateProject.Web.Controllers.Security
{
    [Route("security")]
    public sealed class SecurityController : Controller
    {
        private readonly IDatabaseService _db;
        private readonly IFactory<User> _usersFactory;
        private readonly IFactory<UserRole> _userRolesFactory;
        private readonly IFactory<RefreshToken> _refreshTokenFactory;
        private readonly IWebSecurityService _webSecurityService;
        private readonly IWebConfiguration _webConfig;
        private readonly IMd5Crypter _md5Crypter;

        public SecurityController(IDatabaseService db,
            IFactory<User> usersFactory,
            IFactory<UserRole> userRolesFactory,
            IFactory<RefreshToken> refreshTokenFactory,
            IWebSecurityService webSecurityService,
            IWebConfiguration webConfig,
            IMd5Crypter md5Crypter)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (usersFactory == null)
                throw new ArgumentNullException(nameof(usersFactory));
            if (userRolesFactory == null)
                throw new ArgumentNullException(nameof(userRolesFactory));
            if (refreshTokenFactory == null)
                throw new ArgumentNullException(nameof(refreshTokenFactory));
            if (webSecurityService == null)
                throw new ArgumentNullException(nameof(webSecurityService));
            if (webConfig == null)
                throw new ArgumentNullException(nameof(webConfig));
            if (md5Crypter == null)
                throw new ArgumentNullException(nameof(md5Crypter));

            _db = db;
            _usersFactory = usersFactory;
            _userRolesFactory = userRolesFactory;
            _refreshTokenFactory = refreshTokenFactory;
            _webSecurityService = webSecurityService;
            _webConfig = webConfig;
            _md5Crypter = md5Crypter;
        }

        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInApiModel model)
        {
            if (model == null)
                return BadRequest();
            if (string.IsNullOrWhiteSpace(model.Login) ||
                string.IsNullOrWhiteSpace(model.Password))
                return BadRequest();

            var passwordEncrypted = _md5Crypter.Encrypt(model.Password);

            var user = _db.UsersRepository.Get(model.Login, passwordEncrypted);
            if (user == null)
                return BadRequest();

            var accessToken = _webSecurityService.GetAccessTokenJwt(user);
            var refreshToken = GetRefreshTokenJwt(user);

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterApiModel model)
        {
            if (model == null)
                return BadRequest();
            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.EMail) ||
                string.IsNullOrWhiteSpace(model.Password))
                return BadRequest();

            var users = _db.UsersRepository.GetByNameOrEMail(model.Name, model.EMail);
            if (users.Count > 0)
                return BadRequest();

            var newUser = _usersFactory.Create();
            newUser.Name = model.Name;
            newUser.EMail = model.EMail;
            newUser.PasswordEncrypted = _md5Crypter.Encrypt(model.Password);
            newUser.Roles = GetUserRoles();

            _db.UsersRepository.Add(newUser);

            _db.SaveChanges();

            var accessToken = _webSecurityService.GetAccessTokenJwt(newUser);
            var refreshToken = GetRefreshTokenJwt(newUser);

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        [Route("refreshToken")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] RefreshTokenApiModel model)
        {
            if (model == null)
                return BadRequest();
            if (string.IsNullOrWhiteSpace(model.RefreshToken))
                return BadRequest();

            long tokenId;
            try
            {
                tokenId = _webSecurityService.GetRefreshTokenId(model.RefreshToken);
            }
            catch (JwtTokenInvalidException e)
            {
                return BadRequest(e.Message);
            }

            var refreshToken = _db.RefreshTokensRepository.GetById(tokenId);
            if (refreshToken == null)
                return BadRequest("Token not found");

            var utcNow = DateTime.UtcNow;

            if (refreshToken.ExpiresUtc <= utcNow)
            {
                _db.RefreshTokensRepository.DeleteById(refreshToken.Id.Value);
                _db.SaveChanges();

                return BadRequest("Token is expired");
            }

            refreshToken.ExpiresUtc = utcNow.Add(_webConfig.JwtRefreshTokenLifetime);

            _db.RefreshTokensRepository.AddOrUpdate(refreshToken);
            _db.SaveChanges();

            var user = _db.UsersRepository.GetById(refreshToken.UserId);

            var accessToken = _webSecurityService.GetAccessTokenJwt(user);
            var refreshTokenJwt = _webSecurityService.GetRefreshTokenJwt(refreshToken);

            return Ok(new
            {
                accessToken,
                refreshToken = refreshTokenJwt
            });
        }

        [Route("signout")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignOut([FromBody] SignOutApiModel model)
        {
            if (model == null)
                return BadRequest();
            if (string.IsNullOrWhiteSpace(model.RefreshToken))
                return BadRequest();

            long tokenId;
            try
            {
                tokenId = _webSecurityService.GetRefreshTokenId(model.RefreshToken);
            }
            catch (JwtTokenInvalidException e)
            {
                return BadRequest(e.Message);
            }

            var refreshToken = _db.RefreshTokensRepository.GetById(tokenId);
            if (refreshToken == null)
                return BadRequest("Token not found");

            _db.RefreshTokensRepository.DeleteById(refreshToken.Id.Value);
            _db.SaveChanges();

            return Ok();
        }

        private string GetRefreshTokenJwt(IUser user)
        {
            if (!user.Id.HasValue)
                throw new InvalidOperationException("Cannot create a refresh token for user without id");

            var now = DateTime.UtcNow;
            var expiresUtc = now.Add(_webConfig.JwtRefreshTokenLifetime);

            var refreshToken = _refreshTokenFactory.Create();
            refreshToken.UserId = user.Id.Value;
            refreshToken.ExpiresUtc = expiresUtc;

            _db.RefreshTokensRepository.AddOrUpdate(refreshToken);

            _db.SaveChanges();

            return _webSecurityService.GetRefreshTokenJwt(refreshToken);
        }

        private IList<IUserRole> GetUserRoles()
        {
            var userRole = _userRolesFactory.Create();
            userRole.Name = UserRoleNames.RoleUser;

            return new List<IUserRole>()
            {
                userRole
            };
        }
    }
}