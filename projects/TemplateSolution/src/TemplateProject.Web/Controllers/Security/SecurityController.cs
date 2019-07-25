using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Utils.Factories;
using TemplateProject.Utils.Md5;
using TemplateProject.Web.Configuration;
using TemplateProject.Web.Security;

namespace TemplateProject.Web.Controllers.Security
{
    [Produces("application/json")]
    [Route(WebConstants.ApiPrefix + "/security")]
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
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _usersFactory = usersFactory ?? throw new ArgumentNullException(nameof(usersFactory));
            _userRolesFactory = userRolesFactory ?? throw new ArgumentNullException(nameof(userRolesFactory));
            _refreshTokenFactory = refreshTokenFactory ?? throw new ArgumentNullException(nameof(refreshTokenFactory));
            _webSecurityService = webSecurityService ?? throw new ArgumentNullException(nameof(webSecurityService));
            _webConfig = webConfig ?? throw new ArgumentNullException(nameof(webConfig));
            _md5Crypter = md5Crypter ?? throw new ArgumentNullException(nameof(md5Crypter));
        }

        /// <summary>
        /// Signs in a user.
        /// </summary>
        /// <param name="model">Signin data</param>
        /// <response code="200">Returns tokens</response>
        /// <response code="400">Returns if there is a validation error</response> 
        [ProducesResponseType(typeof(SignInResponse), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInApiDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var passwordEncrypted = _md5Crypter.Encrypt(model.Password);

            var user = _db.UsersRepository.Get(model.Login, passwordEncrypted);
            if (user == null)
                return UserNotFound();

            var accessToken = _webSecurityService.GetAccessTokenJwt(user);
            var refreshToken = GetRefreshTokenJwt(user);

            return Ok(new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="model">Register data</param>
        /// <response code="200">Returns tokens of new logged user</response>
        /// <response code="400">Returns if there is a validation error</response> 
        [ProducesResponseType(typeof(SignInResponse), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterApiDto model)
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

            return Ok(new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Gets a new pair of tokens for new user.
        /// </summary>
        /// <remarks>
        /// Use this method to silently refresh tokens of logged user without sending the password
        /// </remarks>
        /// <param name="model">Refresh token data</param>
        /// <response code="200">Returns new pair of tokens</response>
        /// <response code="400">Returns if there is a validation error</response> 
        [ProducesResponseType(typeof(SignInResponse), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Route("refreshToken")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] RefreshTokenApiDto model)
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

            return Ok(new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenJwt
            });
        }

        /// <summary>
        /// Signs out a user.
        /// </summary>
        /// <param name="model">Sign out data</param>
        /// <response code="200">Returns if the user has been signed out successfully</response>
        /// <response code="400">Returns if there is a validation error</response> 
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Route("signout")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignOut([FromBody] SignOutApiDto model)
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

        public sealed class SignInResponse
        {
            public string AccessToken { get; set; }

            public string RefreshToken { get; set; }
        }
    }
}