namespace Cephalopod.Contracts.Images;

public record UploadImageResponse
{
    public required string RelativePath { get; init; }
    public required string AbsolutePath { get; init; }
}