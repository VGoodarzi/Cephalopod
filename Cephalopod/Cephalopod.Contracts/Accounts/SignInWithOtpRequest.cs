namespace Cephalopod.Contracts.Accounts;

public record SignInWithOtpRequest(string PhoneNumber, string Code);