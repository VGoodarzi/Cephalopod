namespace Cephalopod.Client.Contracts;

public record SignInWithPasswordRequest
{
    public required string UserName { get; init; }
    public required string Password { get; init; }
}