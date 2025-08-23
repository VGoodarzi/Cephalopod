namespace Cephalopod.Contracts.Brands;

public record CreateBrandRequest(string Name, string? Description, string? ImageUrl);