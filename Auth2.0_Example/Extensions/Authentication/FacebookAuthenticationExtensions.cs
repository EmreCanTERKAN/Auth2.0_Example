using Microsoft.AspNetCore.Authentication;

namespace Auth2._0_Example.Extensions.Authentication;

public static class FacebookAuthenticationExtensions
{
    public static AuthenticationBuilder AddFacebookAuth(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        return builder.AddFacebook(options =>
        {
            options.AppId = configuration["FACEBOOK_APP_ID"]!;
            options.AppSecret = configuration["FACEBOOK_APP_SECRET"]!;
            options.CallbackPath = new PathString("/signin-facebook");
        });
    }
}
