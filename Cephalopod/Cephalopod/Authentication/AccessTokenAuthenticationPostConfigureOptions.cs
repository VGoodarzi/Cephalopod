using Microsoft.Extensions.Options;

namespace Cephalopod.Authentication;

internal class AccessTokenAuthenticationPostConfigureOptions 
    : IPostConfigureOptions<AccessTokenAuthenticationOptions>
{
    public void PostConfigure(string? name, AccessTokenAuthenticationOptions options) { }
}