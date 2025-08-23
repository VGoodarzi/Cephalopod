using Cephalopod.Contracts.Utilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Octopus.Client;

internal abstract class BaseOctopusClient(
    HttpClient httpClient,
    ILoggerFactory loggerFactory,
    ITranslator translator)
{
    private readonly ILogger<BaseOctopusClient> _logger
        = loggerFactory.CreateLogger<BaseOctopusClient>();

    protected async Task<Result<TResponse, ProblemDetails>> PostAsync<TRequest, TResponse>(
        TRequest request, string url, CancellationToken cancellationToken)
        where TRequest : notnull
        where TResponse : notnull
    {
        HttpResponseMessage? response = null;
        try
        {
            var content = request.ToStringContent();
            response = await httpClient.PostAsync(url, content, cancellationToken);

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

    protected async Task<Result<ProblemDetails>> PostAsync<TRequest>(
        TRequest request, string url, CancellationToken cancellationToken)
        where TRequest : notnull
    {
        HttpResponseMessage? response = null;
        try
        {
            var content = request.ToStringContent();
            response = await httpClient.PostAsync(url, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return true;
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

    protected async Task<Result<ProblemDetails>> PutAsync<TRequest>(
        TRequest request, string url, CancellationToken cancellationToken)
        where TRequest : notnull
    {
        HttpResponseMessage? response = null;
        try
        {
            var content = request.ToStringContent();
            response = await httpClient.PutAsync(url, content, cancellationToken);

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

    protected async Task<Result<TResponse, ProblemDetails>> GetAsync<TResponse>(
        string url, CancellationToken cancellationToken)
        where TResponse : notnull
    {
        HttpResponseMessage? response = null;
        try
        {
            response = await httpClient.GetAsync(url, cancellationToken);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadFromJsonAsync<TResponse>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, typeof(TResponse).Name);

                return result;
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return ProblemDetails.Error(translator, url, response.StatusCode);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return ProblemDetails.Error(translator, url, response.StatusCode);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {

                var result = await response.Content.ReadFromJsonAsync<ProblemDetails>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, nameof(ProblemDetails));

                return result;
            }

            _logger.LogError("Error occurred on calling url: '{url}', request: '{request}'",
                url, await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken));

            return ProblemDetails.Error(translator, url, response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred calling url: '{url}'", url);
            return ProblemDetails.Error(translator, url, response?.StatusCode ?? 0);
        }
    }
}