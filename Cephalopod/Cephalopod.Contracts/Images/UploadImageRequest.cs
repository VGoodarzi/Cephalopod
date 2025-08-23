namespace Cephalopod.Contracts.Images;

public record UploadImageRequest(string Directory, Stream Stream);