using Cephalopod.Contracts.Utilities;

namespace Cephalopod.Contracts.Accounts;

public interface IAccountService
{
    Task<Result<SignInResponse, string>> SignInWithPassword(
        SignInWithPasswordRequest request, CancellationToken cancellationToken);
}