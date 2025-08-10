namespace Cephalopod.Client.Contracts;

public interface IUserService
{
    Task<SignInResponse> SignInWithPassword(SignInWithPasswordRequest request, CancellationToken cancellationToken);
}