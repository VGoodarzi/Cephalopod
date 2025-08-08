namespace Cephalopod.Client.Accounts;

public interface IUserService
{
    bool IsAuthenticated { get; }
}