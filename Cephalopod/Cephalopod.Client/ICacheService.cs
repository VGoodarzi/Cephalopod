namespace Cephalopod.Client;

public interface ICacheService
{
    Task<string> Get(string key);
    Task Set(string key, string value);
    Task Delete(string key);
}