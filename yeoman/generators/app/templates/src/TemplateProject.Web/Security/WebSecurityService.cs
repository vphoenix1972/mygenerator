﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Web.Configuration;

namespace <%= projectNamespace %>.Web.Security
{
    public sealed class WebSecurityService : IWebSecurityService
    {
        private readonly IWebConfiguration _webConfig;

        public WebSecurityService(IWebConfiguration webConfig)
        {
            if (webConfig == null)
                throw new ArgumentNullException(nameof(webConfig));

            _webConfig = webConfig;
        }

        public TokenValidationParameters JwtTokenValidationParameters
        {
            get
            {
                return new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = WebSecurityConstants.JwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = WebSecurityConstants.JwtAudience,
                    ValidateLifetime = true,
                    IssuerSigningKey = GetJwtSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = _webConfig.JwtClockSkew
                };
            }
        }

        public string GetAccessTokenJwt(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var now = DateTime.UtcNow;

            var identity = GetIdentity(user);

            var jwt = new JwtSecurityToken(
                issuer: WebSecurityConstants.JwtIssuer,
                audience: WebSecurityConstants.JwtAudience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(_webConfig.JwtAccessTokenLifetime),
                signingCredentials: new SigningCredentials(GetJwtSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GetIdentity(IUser user)
        {
            var identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(WebSecurityConstants.AccessTokenUserIdClaim, user.Id));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name));

            var roleClaims = user.Roles.Select(e => new Claim(ClaimsIdentity.DefaultRoleClaimType, e.Name));
            identity.AddClaims(roleClaims);

            return identity;
        }

        public string GetRefreshTokenJwt(IRefreshToken refreshToken)
        {
            var now = DateTime.UtcNow;

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(WebSecurityConstants.RefreshTokenTokenIdClaim, refreshToken.Id.ToString()));

            var jwt = new JwtSecurityToken(
                issuer: WebSecurityConstants.JwtIssuer,
                audience: WebSecurityConstants.JwtAudience,
                notBefore: now,
                claims: identity.Claims,
                expires: refreshToken.ExpiresUtc,
                signingCredentials: new SigningCredentials(GetJwtSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public string GetRefreshTokenId(string refreshTokenJwt)
        {
            if (refreshTokenJwt == null)
                throw new ArgumentNullException(nameof(refreshTokenJwt));

            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal;

            try
            {
                principal = tokenHandler.ValidateToken(refreshTokenJwt, JwtTokenValidationParameters, out _);
            }
            catch (Exception e)
            {
                throw new JwtTokenInvalidException("Token is invalid", e);
            }

            var tokenIdClaim =
                principal.Claims.FirstOrDefault(e => e.Type == WebSecurityConstants.RefreshTokenTokenIdClaim);
            if (tokenIdClaim == null)
                throw new JwtTokenInvalidException("Token doesn't have token_id claim");

            return tokenIdClaim.Value;
        }

        public string GetUserIdFromIdentity(ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userIdClaim = user.Claims.FirstOrDefault(e => e.Type == WebSecurityConstants.AccessTokenUserIdClaim);
            if (userIdClaim == null)
                return null;

            return userIdClaim.Value;
        }

        private SymmetricSecurityKey GetJwtSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_webConfig.JwtKey));
        }
    }
}