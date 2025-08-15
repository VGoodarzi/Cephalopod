using Cephalopod.Contracts.Accounts;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Cephalopod.Client.Contracts;
using Cephalopod.Contracts.Utilities;

namespace Octopus.Client.Accounts;

internal class OctopusAccountService(
    IHttpClientFactory httpClientFactory,
    ILogger<OctopusAccountService> logger, 
    ITranslator translator) : IAccountService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(OctopusConstants.DefaultClient);

    public async Task<Result<SignInResponse, string>> SignInWithPassword(
        SignInWithPasswordRequest request, CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/password/sign-in";
        try
        {
            var content = request.ToStringContent();
            var response = await _httpClient.PostAsync(url, content, cancellationToken);

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
                
                return result.Title;
            }

            logger.LogError("Error occurred on calling url: '{url}', request: '{request}', response: {response}",
                url, JsonSerializer.Serialize(request), await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken));

            return translator["ServerResponseError"];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred calling url: '{url}', request: '{request}'",
                url, JsonSerializer.Serialize(request));
            return translator["ServerError"];
        }
    }
}