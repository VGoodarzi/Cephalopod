using Cephalopod.Contracts.Accounts;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Cephalopod.Contracts.Utilities;

namespace Octopus.Client.Accounts;

internal class OctopusAccountManager(
    IHttpClientFactory httpClientFactory,
    ILogger<OctopusAccountManager> logger,
    ITranslator translator) : IAccountManager
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(OctopusConstants.DefaultClient);

    public async Task<Result<SignInResponse, ProblemDetails>> SignIn(
        SignInWithPasswordRequest request, CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/password/sign-in";
        return await SignInCore(request, url, cancellationToken);
    }

    public async Task<Result<SignInResponse, ProblemDetails>> SignIn(SignInWithRefreshTokenRequest request, CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/refresh-token";
        return await SignInCore(request, url, cancellationToken);
    }

    public async Task<Result<SignInResponse, ProblemDetails>> SignIn(SignInWithOtpRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<Result<SignInResponse, ProblemDetails>> SignInCore(
        object request, string url, CancellationToken cancellationToken)
    {
        HttpResponseMessage? response = null!;
        try
        {
            var content = request.ToStringContent();
            response = await _httpClient.PostAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SignInResponse>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, nameof(SignInResponse));

                return result;
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {

                var result = await response.Content.ReadFromJsonAsync<ProblemDetails>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, nameof(ProblemDetails));

                return result;
            }

            logger.LogError("Error occurred on calling url: '{url}', request: '{request}', response: {response}",
                url, JsonSerializer.Serialize(request), await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken));

            return ProblemDetails.Error(translator, url, response.StatusCode);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred calling url: '{url}', request: '{request}'",
                url, JsonSerializer.Serialize(request));
            return ProblemDetails.Error(translator, url, response?.StatusCode ?? 0); ;
        }
    }
}