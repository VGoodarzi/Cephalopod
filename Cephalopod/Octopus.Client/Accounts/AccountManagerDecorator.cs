using Cephalopod.Contracts.Accounts;
using Cephalopod.Contracts.Utilities;

namespace Octopus.Client.Accounts;

internal class AccountManagerDecorator(HttpClient httpClient, IAccountManager accountManager) : IAccountManager
{
    public Task<Result<SignInResponse, ProblemDetails>> SignIn(SignInWithPasswordRequest request,
        CancellationToken cancellationToken)
    {
        return accountManager.SignIn(request, cancellationToken);
    }

    public Task<Result<SignInResponse, ProblemDetails>> SignIn(SignInWithRefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        return accountManager.SignIn(request, cancellationToken);
    }

    public Task<Result<SignInResponse, ProblemDetails>> SignIn(SignInWithOtpRequest request,
        CancellationToken cancellationToken)
    {
        return accountManager.SignIn(request, cancellationToken);
    }

    public Task<Result<SendOtpResponse, ProblemDetails>> SendOtp(SendOtpRequest request,
        CancellationToken cancellationToken)
    {
        return accountManager.SendOtp(request, cancellationToken);
    }

    public Task<Result<ProblemDetails>> SetPassword(SetPasswordRequest request, CancellationToken cancellationToken)
    {
        return accountManager.SetPassword(request, cancellationToken);
    }

    public async Task<Result<ProblemDetails>> SetPassword(SetPasswordRequest request, string temporaryAccessToken,
        CancellationToken cancellationToken)
    {
        if (httpClient.DefaultRequestHeaders.Contains("Authorization"))
            httpClient.DefaultRequestHeaders.Remove("Authorization");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {temporaryAccessToken}");

        var result = await accountManager.SetPassword(request, temporaryAccessToken, cancellationToken);

        httpClient.DefaultRequestHeaders.Remove("Authorization");

        return result;
    }

    public Task<Result<ProblemDetails>> ChangePassword(ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        return accountManager.ChangePassword(request, cancellationToken);
    }
}