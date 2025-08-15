using Cephalopod.Contracts.Utilities;

namespace Cephalopod.Contracts.Accounts;

public interface IAccountManager
{
    Task<Result<SignInResponse, ProblemDetails>> SignIn(
        SignInWithPasswordRequest request, CancellationToken cancellationToken);

    Task<Result<SignInResponse, ProblemDetails>> SignIn(
        SignInWithRefreshTokenRequest request, CancellationToken cancellationToken);

    Task<Result<SignInResponse, ProblemDetails>> SignIn(
        SignInWithOtpRequest request, CancellationToken cancellationToken);
}