using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.Web.Controllers.Security
{
    [Route("security")]
    public sealed class SecurityController : Controller
    {
        private readonly IDatabaseService _db;

        public SecurityController(IDatabaseService db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _db = db;
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
    }
}