using Cephalopod.Contracts.Brands;
using Cephalopod.Contracts.Utilities;
using Microsoft.Extensions.Logging;

namespace Octopus.Client.Brands;

internal class OctopusBrandService(
    HttpClient httpClient,
    ITranslator translator,
    ILoggerFactory loggerFactory
) : BaseOctopusClient(httpClient, loggerFactory, translator), IBrandService
{
    public async Task<Result<PagedResponse<BrandItemResponse>, ProblemDetails>> GetBrands(
        GetBrandRequest request, CancellationToken cancellationToken)
    {
        const string url = "/api/catalog/brands/page/{0}?page_size={1}";
        var requestUrl = string.Format(url, request.PageNumber, request.PageSize);
        return await base.GetAsync<PagedResponse<BrandItemResponse>>(requestUrl, cancellationToken);
    }

    public async Task<Result<ProblemDetails>> CreateBrand(GetBrandRequest request, CancellationToken cancellationToken)
    {
        const string url = "/api/catalog/brands";
        return await base.PostAsync(request, url, cancellationToken);
    }
}