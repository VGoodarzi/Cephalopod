namespace Cephalopod.Contracts.Accounts;

public interface IAuthenticationService
{
    Task<bool> IsAuthenticated();
    Task<string?> GetUsername();
    Task<string?> GetAccessToken();
}