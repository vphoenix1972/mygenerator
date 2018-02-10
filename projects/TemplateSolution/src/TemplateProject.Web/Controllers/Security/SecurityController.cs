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

        public SecurityController(IDatabaseService db,
            IFactory<User> usersFactory,
            IFactory<UserRole> userRolesFactory)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (usersFactory == null)
                throw new ArgumentNullException(nameof(usersFactory));
            if (userRolesFactory == null)
                throw new ArgumentNullException(nameof(userRolesFactory));

            _db = db;
            _usersFactory = usersFactory;
            _userRolesFactory = userRolesFactory;
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

            var accessToken = GetAccessToken(user);

            return Ok(new
            {
                accessToken
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

            var accessToken = GetAccessToken(newUser);

            return Ok(new
            {
                accessToken
            });
        }

        [Route("validateToken")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateToken([FromBody] ValidateTokenApiModel model)
        {
            if (model == null)
                return BadRequest();
            if (string.IsNullOrWhiteSpace(model.AccessToken))
                return BadRequest();

            await HttpContext.AuthenticateAsync();

            return User.Identity.IsAuthenticated ? Ok() as IActionResult : Unauthorized();
        }

        private string GetAccessToken(IUser user)
        {
            var now = DateTime.UtcNow;

            var identity = GetIdentity(user);

            var jwt = new JwtSecurityToken(
                issuer: WebProjectConstants.JwtIssuer,
                audience: WebProjectConstants.JwtAudience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(WebProjectConstants.JwtLifetime),
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