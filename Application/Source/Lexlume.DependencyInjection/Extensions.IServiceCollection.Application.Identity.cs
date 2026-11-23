using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lexlume.DependencyInjection;

public static partial class Extensions
{
    private static IServiceCollection ApplicationIdentityService(this IServiceCollection services)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.Cookie.Name = "Lexlume.Auth";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opt.Cookie.SameSite = SameSiteMode.Lax;

                opt.ExpireTimeSpan = TimeSpan.FromDays(7);
                opt.SlidingExpiration = true;

                // APIs should return status codes, not login-page redirects.
                opt.Events.OnRedirectToLogin = ctx =>
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };

                opt.Events.OnRedirectToAccessDenied = ctx =>
                {
                    ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            });

        services.AddAuthorization();

        return services;
    }
}