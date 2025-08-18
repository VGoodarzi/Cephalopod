namespace Cephalopod.Contracts.Accounts;

public readonly record struct SignInResponse
{
    public required string TokenType { get; init; }
    public int ExpireIn { get; init; }
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required string PhoneNumber { get; init; }
    public required string UserId { get; init; }
    public bool HasPassword { get; init; }
    public DateTimeOffset ExpiredAt => DateTimeOffset.FromUnixTimeSeconds(ExpireIn);
}