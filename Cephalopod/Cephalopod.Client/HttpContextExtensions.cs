using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cephalopod.Client;

public static class HttpContextExtensions
{
    public static string? GetName(this ClaimsPrincipal user)
    {
        return user.Claims
            .FirstOrDefault(c => JwtRegisteredClaimNames.GivenName.Equals(c.Type, StringComparison.OrdinalIgnoreCase))
            ?.Value;
    }
}