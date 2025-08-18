namespace Cephalopod.Contracts.Accounts;

public readonly record struct SendOtpResponse
{
    public TimeSpan Expires { get; init; }
}