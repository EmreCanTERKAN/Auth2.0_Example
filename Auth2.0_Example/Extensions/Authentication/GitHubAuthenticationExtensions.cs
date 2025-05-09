using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text.Json;

namespace Auth2._0_Example.Extensions.Authentication;

public static class GitHubAuthenticationExtensions
{
    public static AuthenticationBuilder AddGitHubAuth(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        return builder.AddOAuth("GitHub", options =>
        {
            options.ClientId = configuration["GITHUB_CLIENT_ID"]!;
            options.ClientSecret = configuration["GITHUB_CLIENT_SECRET"]!;
            options.CallbackPath = new PathString("/signin-github");

            options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
            options.TokenEndpoint = "https://github.com/login/oauth/access_token";
            options.UserInformationEndpoint = "https://api.github.com/user";

            options.SaveTokens = true;

            options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
            options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
            options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

            options.Events = new OAuthEvents
            {
                OnCreatingTicket = async context =>
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
                    request.Headers.Add("User-Agent", "GitHubOAuthApp");

                    var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                    response.EnsureSuccessStatusCode();

                    using var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    context.RunClaimActions(user.RootElement);
                }
            };
        });
    }
}