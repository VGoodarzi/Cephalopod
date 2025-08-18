namespace Cephalopod.Contracts.Accounts;

public record ChangePasswordRequest(string NewPassword, string OldPassword);