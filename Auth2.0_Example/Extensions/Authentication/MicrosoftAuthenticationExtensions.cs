using Microsoft.AspNetCore.Authentication;

namespace Auth2._0_Example.Extensions.Authentication;

public static class MicrosoftAuthenticationExtensions
{
    public static AuthenticationBuilder AddMicrosoftAuth(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        return builder.AddMicrosoftAccount(options =>
        {
            options.ClientId = configuration["MS_CLIENT_ID"]!;
            options.ClientSecret = configuration["MS_CLIENT_SECRET"]!;
            options.CallbackPath = new PathString("/signin-microsoft");
        });
    }
}
