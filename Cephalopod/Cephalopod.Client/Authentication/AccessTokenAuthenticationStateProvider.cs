using Cephalopod.Contracts.Accounts;
using Cephalopod.Contracts.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Cephalopod.Client.Authentication;

internal class AccessTokenAuthenticationStateProvider(
    ICacheService cacheService,
    ILogger<AccessTokenAuthenticationStateProvider> logger)
    : AuthenticationStateProvider, IAuthenticationNotify
{

    private readonly JwtSecurityTokenHandler _handler = new();

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        logger.LogWarning("user has request");
        var token = await cacheService.Get(BlazorConstants.AccessTokenCookieName);

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var jwt = _handler.ReadJwtToken(token);

        if (jwt is null)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        if (HasExpired(jwt))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var identity = new ClaimsIdentity(jwt.Claims, BlazorConstants.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);
    }

    private bool HasExpired(JwtSecurityToken jwt)
    {
        var expClaim = jwt.Claims.SingleOrDefault(c => c.Type == "exp");
        if (expClaim is null) return false;

        if (!int.TryParse(expClaim.Value, out var epochTime)) return false;

        var expires = DateTimeOffset.FromUnixTimeSeconds(epochTime);

        return expires < DateTimeOffset.Now;
    }

    public Task LoggedIn(DateTimeOffset expires)
    {
        Notify();
        SetTimer(expires);
        return Task.CompletedTask;
    }

    public Task LoggedOut()
    {
        Notify();
        return Task.CompletedTask;
    }

    private void SetTimer(DateTimeOffset expires)
    {
        TimeSpan timeToWait = expires - DateTimeOffset.Now;

        _timer?.Dispose();

        if (timeToWait.TotalMilliseconds > 0)
        {
            _timer = new Timer(_ =>
                {
                    Notify();
                    _timer?.Dispose();
                },
                null, timeToWait, Timeout.InfiniteTimeSpan);
        }
    }

    private void Notify() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private Timer? _timer;
}