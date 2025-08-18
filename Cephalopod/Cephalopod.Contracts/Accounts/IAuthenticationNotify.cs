namespace Cephalopod.Contracts.Accounts;

public interface IAuthenticationNotify
{
    Task LoggedIn(DateTimeOffset expires);
    Task LoggedOut();
}