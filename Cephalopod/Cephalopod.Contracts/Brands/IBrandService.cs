using Cephalopod.Contracts.Utilities;

namespace Cephalopod.Contracts.Brands;

public interface IBrandService
{
    public Task<Result<PagedResponse<BrandItemResponse>, ProblemDetails>> GetBrands(GetBrandRequest request,
        CancellationToken cancellationToken);

    public Task<Result<ProblemDetails>> CreateBrand(GetBrandRequest request,
        CancellationToken cancellationToken);
}