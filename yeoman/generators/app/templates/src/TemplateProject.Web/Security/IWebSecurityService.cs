using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Security
{
    public interface IWebSecurityService
    {
        TokenValidationParameters JwtTokenValidationParameters { get; }

        string GetAccessTokenJwt(IUser user);

        ClaimsIdentity GetIdentity(IUser user);

        string GetRefreshTokenJwt(IRefreshToken refreshToken);

        long GetRefreshTokenId(string refreshTokenJwt);
    }
}