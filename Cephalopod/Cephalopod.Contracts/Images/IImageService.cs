using Cephalopod.Contracts.Utilities;

namespace Cephalopod.Contracts.Images;

public interface IImageService
{
    Task<Result<UploadImageResponse, ProblemDetails>> Upload(UploadImageRequest request, CancellationToken cancellationToken);
}