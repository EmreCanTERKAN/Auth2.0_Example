using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using YourApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGitHubAuthentication(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", async context =>
{
    if (context.User.Identity!.IsAuthenticated)
    {
        var name = context.User.Identity?.Name;
        var profileUrl = context.User.FindFirst("urn:github:url")?.Value;
        var avatarUrl = context.User.FindFirst("urn:github:avatar")?.Value;

        await context.Response.WriteAsync($"""
            <h1>Merhaba, {name}!</h1>
            <img src="{avatarUrl}" width="100" />
            <p><a href="{profileUrl}" target="_blank">GitHub Profiline Git</a></p>
            <a href="/logout">Çýkýþ Yap</a>
        """);
    }
    else
    {
        await context.Response.WriteAsync("<a href='/login'>GitHub ile Giriþ Yap</a>");
    }
});

app.MapGet("/login", async context =>
{
    await context.ChallengeAsync("GitHub", new AuthenticationProperties { RedirectUri = "/" });
});

app.MapGet("/logout", async context =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

app.Run();
