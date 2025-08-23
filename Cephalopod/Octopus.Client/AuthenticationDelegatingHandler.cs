using Cephalopod.Contracts.Accounts;

namespace Octopus.Client;

internal class AuthenticationDelegatingHandler(IAuthenticationService authenticationService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await TryAuthenticate(request);

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<bool> TryAuthenticate(HttpRequestMessage request)
    {
        var accessToken = request.Headers.Authorization?.Parameter;

        if (accessToken is not null) return true;

        if (!await authenticationService.IsAuthenticated()) return false;

        accessToken = await authenticationService.GetAccessToken();
        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        return true;
    }
}