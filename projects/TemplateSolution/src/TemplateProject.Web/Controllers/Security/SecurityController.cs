using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.Utils.Factories;

namespace TemplateProject.Web.Controllers.Security
{
    [Route("security")]
    public sealed class SecurityController : Controller
    {
        private readonly IDatabaseService _db;
        private readonly IFactory<User> _usersFactory;
        private readonly IFactory<UserRole> _userRolesFactory;
        private readonly IFactory<RefreshToken> _refreshTokenFactory;

        public SecurityController(IDatabaseService db,
            IFactory<User> usersFactory,
            IFactory<UserRole> userRolesFactory,
            IFactory<RefreshToken> refreshTokenFactory)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (usersFactory == null)
                throw new ArgumentNullException(nameof(usersFactory));
            if (userRolesFactory == null)
                throw new ArgumentNullException(nameof(userRolesFactory));
            if (refreshTokenFactory == null)
                throw new ArgumentNullException(nameof(refreshTokenFactory));

            _db = db;
            _usersFactory = usersFactory;
            _userRolesFactory = userRolesFactory;
            _refreshTokenFactory = refreshTokenFactory;
        }

        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInApiModel model)
        {
            if (model == null)
                return BadRequest();

            var user = _db.UsersRepository.Get(model.Login, model.Password);
            if (user == null)
                return BadRequest();

            var accessToken = GetAccessTokenJwt(user);
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
            newUser.Password = model.Password;
            newUser.Roles = GetUserRoles();

            _db.UsersRepository.Add(newUser);

            _db.SaveChanges();

            var accessToken = GetAccessTokenJwt(newUser);
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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenApiModel model)
        {
            if (model == null)
                return BadRequest();
            if (string.IsNullOrWhiteSpace(model.RefreshToken))
                return BadRequest();

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = WebProjectConstants.JwtIssuer,
                ValidateAudience = true,
                ValidAudience = WebProjectConstants.JwtAudience,
                ValidateLifetime = true,
                IssuerSigningKey = WebProjectConstants.GetJwtSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = WebProjectConstants.JwtClockSkew
            };

            ClaimsPrincipal principal;

            try
            {
                principal = tokenHandler.ValidateToken(model.RefreshToken, tokenValidationParameters, out _);
            }
            catch
            {
                return BadRequest("Token is invalid");
            }

            var tokenIdClaim = principal.Claims.FirstOrDefault(e => e.Type == "token_id");
            if (tokenIdClaim == null)
                return BadRequest("Token doesn't have token_id claim");

            long tokenId;
            if (!long.TryParse(tokenIdClaim.Value, out tokenId))
                return BadRequest("token_id claim is invalid");

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

            refreshToken.ExpiresUtc = utcNow.Add(WebProjectConstants.JwtRefreshTokenLifetime);

            _db.RefreshTokensRepository.AddOrUpdate(refreshToken);
            _db.SaveChanges();

            var user = _db.UsersRepository.GetById(refreshToken.UserId);

            var accessToken = GetAccessTokenJwt(user);
            var refreshTokenJwt = GetRefreshTokenJwt(refreshToken);

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

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = WebProjectConstants.JwtIssuer,
                ValidateAudience = true,
                ValidAudience = WebProjectConstants.JwtAudience,
                ValidateLifetime = true,
                IssuerSigningKey = WebProjectConstants.GetJwtSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                ClockSkew = WebProjectConstants.JwtClockSkew
            };

            ClaimsPrincipal principal;

            try
            {
                principal = tokenHandler.ValidateToken(model.RefreshToken, tokenValidationParameters, out _);
            }
            catch
            {
                return BadRequest("Token is invalid");
            }

            var tokenIdClaim = principal.Claims.FirstOrDefault(e => e.Type == "token_id");
            if (tokenIdClaim == null)
                return BadRequest("Token doesn't have token_id claim");

            long tokenId;
            if (!long.TryParse(tokenIdClaim.Value, out tokenId))
                return BadRequest("token_id claim is invalid");

            var refreshToken = _db.RefreshTokensRepository.GetById(tokenId);
            if (refreshToken == null)
                return BadRequest("Token not found");

            _db.RefreshTokensRepository.DeleteById(refreshToken.Id.Value);
            _db.SaveChanges();

            return Ok();
        }

        private string GetAccessTokenJwt(IUser user)
        {
            var now = DateTime.UtcNow;

            var identity = GetIdentity(user);

            var jwt = new JwtSecurityToken(
                issuer: WebProjectConstants.JwtIssuer,
                audience: WebProjectConstants.JwtAudience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(WebProjectConstants.JwtAccessTokenLifetime),
                signingCredentials: new SigningCredentials(WebProjectConstants.GetJwtSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private string GetRefreshTokenJwt(IUser user)
        {
            if (!user.Id.HasValue)
                throw new InvalidOperationException("Cannot create a refresh token for user without id");

            var now = DateTime.UtcNow;
            var expiresUtc = now.Add(WebProjectConstants.JwtRefreshTokenLifetime);

            var refreshToken = _refreshTokenFactory.Create();
            refreshToken.UserId = user.Id.Value;
            refreshToken.ExpiresUtc = expiresUtc;

            _db.RefreshTokensRepository.AddOrUpdate(refreshToken);

            _db.SaveChanges();

            return GetRefreshTokenJwt(refreshToken);
        }

        private string GetRefreshTokenJwt(IRefreshToken refreshToken)
        {
            var now = DateTime.UtcNow;

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("token_id", refreshToken.Id.ToString()));

            var jwt = new JwtSecurityToken(
                issuer: WebProjectConstants.JwtIssuer,
                audience: WebProjectConstants.JwtAudience,
                notBefore: now,
                claims: identity.Claims,
                expires: refreshToken.ExpiresUtc,
                signingCredentials: new SigningCredentials(WebProjectConstants.GetJwtSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(IUser user)
        {
            var identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name));

            var roleClaims = user.Roles.Select(e => new Claim(ClaimsIdentity.DefaultRoleClaimType, e.Name));
            identity.AddClaims(roleClaims);

            return identity;
        }

        private IList<IUserRole> GetUserRoles()
        {
            var userRole = _userRolesFactory.Create();
            userRole.Name = WebProjectConstants.RoleUser;

            return new List<IUserRole>()
            {
                userRole
            };
        }
    }
}