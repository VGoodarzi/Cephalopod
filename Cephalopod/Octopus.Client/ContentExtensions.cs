using System.Text;
using System.Text.Json;

namespace Octopus.Client;

internal static class ContentExtensions
{
    public static StringContent ToStringContent(this object model)
    {
        var json = JsonSerializer.Serialize(model);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}