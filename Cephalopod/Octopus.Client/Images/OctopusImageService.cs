using Cephalopod.Contracts.Images;
using Cephalopod.Contracts.Utilities;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Octopus.Client.Images;

internal class OctopusImageService(
    HttpClient httpClient,
    ILogger<OctopusImageService> logger,
    ITranslator translator
) : IImageService
{
    public async Task<Result<UploadImageResponse, ProblemDetails>> Upload(UploadImageRequest request, CancellationToken cancellationToken)
    {
        const string url = "/api/file-manager/images";
        HttpResponseMessage? response = null;
        try
        {
            var multipartContent = new MultipartFormDataContent();

            var fileContent = new StreamContent(request.Stream);
            multipartContent.Add(fileContent, "file", Guid.CreateVersion7().ToString("N"));
            multipartContent.Add(new StringContent(request.Directory), "directory");

            response = await httpClient.PostAsync(url, multipartContent, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ProblemDetails>(
                    cancellationToken: cancellationToken);

                ArgumentNullException.ThrowIfNull(result, nameof(ProblemDetails));

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
            return ProblemDetails.Error(translator, url, response?.StatusCode ?? 0);
        }
    }
}