namespace Cephalopod.Client.Accounts;

public class FakeUserService : IUserService
{
    public FakeUserService()
    {
        
    }
    public bool IsAuthenticated => false;
}