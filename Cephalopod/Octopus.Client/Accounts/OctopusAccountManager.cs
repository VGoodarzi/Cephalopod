using Cephalopod.Contracts.Accounts;
using Cephalopod.Contracts.Utilities;
using Microsoft.Extensions.Logging;

namespace Octopus.Client.Accounts;

internal class OctopusAccountManager(
    HttpClient httpClient,
    ILoggerFactory loggerFactory,
    ITranslator translator)
    : BaseOctopusClient(httpClient, loggerFactory, translator), IAccountManager
{
    public async Task<Result<SignInResponse, ProblemDetails>> SignIn(
        SignInWithPasswordRequest request, CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/password/sign-in";
        return await PostAsync<SignInWithPasswordRequest, SignInResponse>(request, url, cancellationToken);
    }

    public async Task<Result<SignInResponse, ProblemDetails>> SignIn(SignInWithRefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/refresh-token";
        return await PostAsync<SignInWithRefreshTokenRequest, SignInResponse>(request, url, cancellationToken);
    }

    public async Task<Result<SignInResponse, ProblemDetails>> SignIn(SignInWithOtpRequest request,
        CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/otp/sign-in";
        return await PostAsync<SignInWithOtpRequest, SignInResponse>(request, url, cancellationToken);
    }

    public async Task<Result<SendOtpResponse, ProblemDetails>> SendOtp(SendOtpRequest request,
        CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/otp/send";
        return await PostAsync<SendOtpRequest, SendOtpResponse>(request, url, cancellationToken);
    }

    public async Task<Result<ProblemDetails>> SetPassword(SetPasswordRequest request,
        CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/password/set";
        return await PutAsync(request, url, cancellationToken);
    }

    public Task<Result<ProblemDetails>> SetPassword(SetPasswordRequest request, string temporaryAccessToken,
        CancellationToken cancellationToken)
    {
        return SetPassword(request, cancellationToken);
    }

    public async Task<Result<ProblemDetails>> ChangePassword(ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        const string url = "/api/user-management/users/password/change";
        return await PutAsync(request, url, cancellationToken);
    }
}