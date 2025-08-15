using Cephalopod.Contracts.Accounts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Cephalopod.Client;

internal class AuthenticationService(
    AuthenticationStateProvider authenticationStateProvider
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

    private async Task<AuthenticationState> GetCurrentState()
    {
        return await authenticationStateProvider.GetAuthenticationStateAsync();
    }
}