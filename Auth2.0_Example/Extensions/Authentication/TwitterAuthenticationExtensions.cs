using Microsoft.AspNetCore.Authentication;

namespace Auth2._0_Example.Extensions.Authentication;

public static class TwitterAuthenticationExtensions
{
    public static AuthenticationBuilder AddTwitterAuth(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        return builder.AddTwitter(options =>
        {
            options.ConsumerKey = configuration["TWITTER_CONSUMER_KEY"]!;
            options.ConsumerSecret = configuration["TWITTER_CONSUMER_SECRET"]!;
            options.CallbackPath = new PathString("/signin-twitter");
        });
    }
}