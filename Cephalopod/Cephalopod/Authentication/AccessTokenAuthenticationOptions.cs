using Microsoft.AspNetCore.Authentication;

namespace Cephalopod.Authentication;

public class AccessTokenAuthenticationOptions : AuthenticationSchemeOptions
{
    public string? LoginPath { get; set; }
}