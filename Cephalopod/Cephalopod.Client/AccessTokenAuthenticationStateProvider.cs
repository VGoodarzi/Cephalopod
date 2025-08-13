using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cephalopod.Client;

internal class AccessTokenAuthenticationStateProvider(ICacheService cacheService) : AuthenticationStateProvider
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

        var identity = new ClaimsIdentity(jwt.Claims, "jwt");
        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);
    }

    private async Task<string?> GetTokenAsync()
    {
        string? token;//= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwMTk4OTM3MS01OTA4LTc0ZjUtODQ1Yy1jMjNmMzBlZTE5ODUiLCJnaXZlbl9uYW1lIjoiYWRtaW4iLCJmYW1pbHlfbmFtZSI6ImFkbWluIiwibmFtZWlkIjoiYWRtaW4iLCJ1aWQiOiI1ZjRjYjc4Ny01OGRhLTRmODMtOGFhYS1iYjQ4ZTRlMjc4YzMiLCJpcCI6IjEyNy4wLjAuMSIsInJvbGVzIjpbIkdsb2JhbFJlYWQiLCJHbG9iYWxXcml0ZSIsIkJhc2ljIl0sInN1YiI6Iis5ODkxMjEwMDAwMDAiLCJleHAiOjE3NTQ4MjM5MzUsImlzcyI6IkNvcmVJZGVudGl0eSIsImF1ZCI6IkNvcmVJZGVudGl0eVVzZXIifQ.uxfl19BD4yoRmwBUOK_9H2yOTmWyg59Rr00pMbTIGp0";
        token = await cacheService.Get(BlazorConstants.AccessTokenCookieName);

        return token;
    }
}