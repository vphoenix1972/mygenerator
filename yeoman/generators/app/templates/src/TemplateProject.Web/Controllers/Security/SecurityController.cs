using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.Utils.Factories;
using <%= projectNamespace %>.Utils.Md5;
using <%= projectNamespace %>.Web.Configuration;
using <%= projectNamespace %>.Web.Security;

namespace <%= projectNamespace %>.Web.Controllers.Security
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
        public IActionResult SignIn([FromBody] SignInModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var passwordEncrypted = _md5Crypter.Encrypt(model.Password);

            var user = _db.UsersRepository.Get(model.Login, passwordEncrypted);
            if (user == null)
                return UserNotFound();

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
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _db.UsersRepository.GetByNameOrEMail(model.Name, model.EMail);
            if (users.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Such user already exists");
                return BadRequest(ModelState);
            }

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
        public IActionResult RefreshToken([FromBody] RefreshTokenModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string tokenId;
            try
            {
                tokenId = _webSecurityService.GetRefreshTokenId(model.RefreshToken);
            }
            catch (JwtTokenInvalidException e)
            {
                return TokenInvalid(e);
            }

            var refreshToken = _db.RefreshTokensRepository.GetById(tokenId);
            if (refreshToken == null)
                return TokenNotFound();

            var utcNow = DateTime.UtcNow;

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
        public IActionResult SignOut([FromBody] SignOutModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string tokenId;
            try
            {
                tokenId = _webSecurityService.GetRefreshTokenId(model.RefreshToken);
            }
            catch (JwtTokenInvalidException e)
            {
                return TokenInvalid(e);
            }

            var refreshToken = _db.RefreshTokensRepository.GetById(tokenId);
            if (refreshToken == null)
                return TokenNotFound();

            _db.RefreshTokensRepository.DeleteById(refreshToken.Id);
            _db.SaveChanges();

            return Ok();
        }

        private string GetRefreshTokenJwt(IUser user)
        {
            if (user.Id == null)
                throw new InvalidOperationException("Cannot create a refresh token for user without id");

            var now = DateTime.UtcNow;
            var expiresUtc = now.Add(_webConfig.JwtRefreshTokenLifetime);

            var refreshToken = _refreshTokenFactory.Create();
            refreshToken.UserId = user.Id;
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

        private IActionResult UserNotFound()
        {
            ModelState.AddModelError(string.Empty, "User is not found");
            return BadRequest(ModelState);
        }

        private IActionResult TokenInvalid(JwtTokenInvalidException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return BadRequest(ModelState);
        }

        private IActionResult TokenNotFound()
        {
            ModelState.AddModelError(string.Empty, "Token is not found");
            return BadRequest(ModelState);
        }
    }
}