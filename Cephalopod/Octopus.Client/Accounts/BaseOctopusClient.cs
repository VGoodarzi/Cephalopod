using Cephalopod.Contracts.Utilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Octopus.Client.Accounts;

internal abstract class BaseOctopusClient(
    IHttpClientFactory httpClientFactory,
    ILoggerFactory loggerFactory,
    ITranslator translator)
{
    private readonly ILogger<BaseOctopusClient> _logger
        = loggerFactory.CreateLogger<BaseOctopusClient>();
    private readonly HttpClient _httpClient
        = httpClientFactory.CreateClient(OctopusConstants.DefaultClient);

    private bool _useTemporaryAccessToken = false;
    private string? _temporaryAccessToken;
    public void WithTemporaryAccessToken(string accessToken)
    {
        _useTemporaryAccessToken = true;
        _temporaryAccessToken = accessToken;
    }

    public async Task<Result<TResponse, ProblemDetails>> PostAsync<TRequest, TResponse>(
        TRequest request, string url, CancellationToken cancellationToken)
        where TRequest : notnull
        where TResponse : notnull
    {
        HttpResponseMessage? response = null!;
        try
        {
            var content = request.ToStringContent();
            response = await _httpClient.PostAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TResponse>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, typeof(TResponse).Name);

                return result;
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {

                var result = await response.Content.ReadFromJsonAsync<ProblemDetails>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, nameof(ProblemDetails));

                return result;
            }

            _logger.LogError("Error occurred on calling url: '{url}', request: '{request}', response: {response}",
                url, JsonSerializer.Serialize(request), await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken));

            return ProblemDetails.Error(translator, url, response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred calling url: '{url}', request: '{request}'",
                url, JsonSerializer.Serialize(request));
            return ProblemDetails.Error(translator, url, response?.StatusCode ?? 0);
        }
    }

    public async Task<Result<ProblemDetails>> PutAsync<TRequest>(
        TRequest request, string url, CancellationToken cancellationToken)
        where TRequest : notnull
    {
        HttpResponseMessage? response = null!;
        try
        {
            var content = request.ToStringContent();

            if (_useTemporaryAccessToken)
            {
                if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_temporaryAccessToken}");

                response = await _httpClient.PutAsync(url, content, cancellationToken);

                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _useTemporaryAccessToken = false;
            }
            else
            {
                response = await _httpClient.PutAsync(url, content, cancellationToken);
            }

            if (response.IsSuccessStatusCode)
                return true;

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {

                var result = await response.Content.ReadFromJsonAsync<ProblemDetails>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, nameof(ProblemDetails));

                return result;
            }

            _logger.LogError("Error occurred on calling url: '{url}', request: '{request}', response: {response}",
                url, JsonSerializer.Serialize(request), await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken));

            return ProblemDetails.Error(translator, url, response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred calling url: '{url}', request: '{request}'",
                url, JsonSerializer.Serialize(request));
            return ProblemDetails.Error(translator, url, response?.StatusCode ?? 0);
        }
    }
}