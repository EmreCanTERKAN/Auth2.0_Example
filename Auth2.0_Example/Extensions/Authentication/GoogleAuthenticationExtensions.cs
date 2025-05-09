using Microsoft.AspNetCore.Authentication;

namespace Auth2._0_Example.Extensions.Authentication;

public static class GoogleAuthenticationExtensions
{
    public static AuthenticationBuilder AddGoogleAuth(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        return builder.AddGoogle(options =>
        {
            options.ClientId = configuration["GOOGLE_CLIENT_ID"]!;
            options.ClientSecret = configuration["GOOGLE_CLIENT_SECRET"]!;
            options.CallbackPath = new PathString("/signin-google");
        });
    }
}
