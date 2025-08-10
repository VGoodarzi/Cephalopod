using Cephalopod.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Cephalopod.Authentication;

internal class AccessTokenAuthenticationHandler(
    IOptionsMonitor<AccessTokenAuthenticationOptions> options,
    ILoggerFactory loggerFactory,
    UrlEncoder encoder
    ) : AuthenticationHandler<AccessTokenAuthenticationOptions>(options, loggerFactory, encoder)
{
    private readonly JwtSecurityTokenHandler _handler = new();

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Cookies.TryGetValue(BlazorConstants.AccessTokenCookieName, out var accessToken))
            return Unauthorized();

        if (string.IsNullOrWhiteSpace(accessToken))
            return Unauthorized();

        var jwt = _handler.ReadJwtToken(accessToken);

        if (jwt is null)
            return Unauthorized();


        var identity = new ClaimsIdentity(jwt.Claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private Task<AuthenticateResult> Unauthorized()
    {
        return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        if (Context.User?.Identity?.IsAuthenticated == false)
        {
            var redirectUri = $"{Options.LoginPath}";
            Response.Redirect(redirectUri);
        }
        else
        {
            await base.HandleChallengeAsync(properties);
        }
    }
}