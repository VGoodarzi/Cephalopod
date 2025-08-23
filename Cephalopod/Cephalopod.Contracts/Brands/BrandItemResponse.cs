namespace Cephalopod.Contracts.Brands;

public record BrandItemResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Status { get; init; }
    public string? ImageUrl { get; init; }
    public string? Description { get; init; }
}