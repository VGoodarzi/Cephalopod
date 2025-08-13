using Microsoft.JSInterop;

namespace Cephalopod.Client;

internal class LocalStorageCacheService(IJSRuntime jsRuntime) : ICacheService
{
    public async Task<string> Get(string key)
    {
        return await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
    }

    public async Task Set(string key, string value)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    public async Task Delete(string key)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}