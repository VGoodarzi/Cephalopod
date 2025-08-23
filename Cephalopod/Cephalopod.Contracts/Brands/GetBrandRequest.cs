namespace Cephalopod.Contracts.Brands;

public record GetBrandRequest
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}