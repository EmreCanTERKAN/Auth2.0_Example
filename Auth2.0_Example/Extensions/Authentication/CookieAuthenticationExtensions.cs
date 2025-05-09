using Microsoft.AspNetCore.Authentication.Cookies;

namespace Auth2._0_Example.Extensions.Authentication;

public static class CookieAuthenticationExtensions
{
    public static IServiceCollection AddAppCookieAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

        return services;
    }
}
