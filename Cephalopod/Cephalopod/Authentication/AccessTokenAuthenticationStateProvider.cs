using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cephalopod.Client;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cephalopod.Authentication;

internal class AccessTokenAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
{

    private readonly JwtSecurityTokenHandler _handler = new();

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await GetTokenAsync();
        
        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var jwt = _handler.ReadJwtToken(token);

        if (jwt is null)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var identity = new ClaimsIdentity(jwt.Claims, AccessTokenAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);
    }

    private async Task<string?> GetTokenAsync()
    {
        string? token = null;
        httpContextAccessor.HttpContext?
            .Request.Cookies.TryGetValue(BlazorConstants.AccessTokenCookieName, out token);

        return await Task.FromResult(token);
    }
}