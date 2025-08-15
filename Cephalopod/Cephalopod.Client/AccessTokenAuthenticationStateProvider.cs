using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cephalopod.Contracts.Utilities;

namespace Cephalopod.Client;

internal class AccessTokenAuthenticationStateProvider : AuthenticationStateProvider
{

    private readonly JwtSecurityTokenHandler _handler = new();
    private readonly ICacheService _cacheService;
    private readonly ILogger<AccessTokenAuthenticationStateProvider> _logger;

    public AccessTokenAuthenticationStateProvider(ICacheService cacheService, ILogger<AccessTokenAuthenticationStateProvider> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
        //AuthenticationStateChanged += OnAuthenticationStateChanged;
    }

    //private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    //{
    //    currentState = await 
    //}

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _logger.LogWarning("user has request");
        var token = await _cacheService.Get(BlazorConstants.AccessTokenCookieName); 

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var jwt = _handler.ReadJwtToken(token);

        if (jwt is null || HasExpired(jwt))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        
        var identity = new ClaimsIdentity(jwt.Claims, "jwt");
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
}