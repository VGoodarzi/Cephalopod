using Cephalopod.Contracts.Accounts;
using Cephalopod.Contracts.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Cephalopod.Client.Authentication;

internal class AuthenticationService(
    AuthenticationStateProvider authenticationStateProvider,
    ICacheService cacheService
) : IAuthenticationService
{
    public async Task<bool> IsAuthenticated()
    {
        var currentState = await GetCurrentState();

        return currentState.User.Identity?.IsAuthenticated ?? false;
    }

    public async Task<string?> GetUsername()
    {
        var currentState = await GetCurrentState();

        return currentState.User.Claims
            .SingleOrDefault(c=>JwtRegisteredClaimNames.NameId.Equals(c.Type))
            ?.Value;
    }

    public async Task<string?> GetAccessToken() 
        => await cacheService.Get(BlazorConstants.AccessTokenCookieName);

    private async Task<AuthenticationState> GetCurrentState() 
        => await authenticationStateProvider.GetAuthenticationStateAsync();
}