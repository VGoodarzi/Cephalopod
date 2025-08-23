namespace Cephalopod.Contracts.Brands;

public record PagedResponse<TModel>
{
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public ICollection<TModel> Items { get; init; } = [];
}