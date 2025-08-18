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

    Task<Result<SendOtpResponse, ProblemDetails>> SendOtp(
        SendOtpRequest request, CancellationToken cancellationToken);

    Task<Result<ProblemDetails>> SetPassword(SetPasswordRequest request, 
        CancellationToken cancellationToken);

    Task<Result<ProblemDetails>> SetPassword(SetPasswordRequest request, string temporaryAccessToken,
        CancellationToken cancellationToken);

    Task<Result<ProblemDetails>> ChangePassword(ChangePasswordRequest request,
        CancellationToken cancellationToken);
}