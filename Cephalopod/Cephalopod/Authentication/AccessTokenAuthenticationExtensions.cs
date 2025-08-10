using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Cephalopod.Authentication;

public static class AccessTokenAuthenticationExtensions
{
    public static AuthenticationBuilder AddCookieAccessToken(this AuthenticationBuilder builder,  
        Action<AccessTokenAuthenticationOptions>? configureOptions = null)
          {
        builder.Services.AddSingleton<IPostConfigureOptions<AccessTokenAuthenticationOptions>, 
            AccessTokenAuthenticationPostConfigureOptions>();

        return builder.AddScheme<AccessTokenAuthenticationOptions, AccessTokenAuthenticationHandler>(
            AccessTokenAuthenticationDefaults.AuthenticationScheme, configureOptions);
    }
}