using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Services.Auth;
using WebMVC.Services.Token;

namespace WebMVC;

public static class MvcServiceRegistrations
{

    public static IServiceCollection AddMvcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("ApiClient", opt =>
        {
            string apiUrl = configuration["ApiUrl"] ?? throw new InvalidOperationException();
            opt.BaseAddress = new Uri(apiUrl);
        });

        services.AddJwtAuthentication(configuration);
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthService, HttpAuthService>();
        services.AddScoped<ITokenService, CookieTokenService>();


        return services;
    }
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()
            ?? throw new InvalidOperationException("TokenOptions cant found in configuration");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                        context.Token = tokenService.GetAccessToken();

                        return Task.CompletedTask;
                    },

                    OnChallenge = async context =>
                    {
                        var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
                        var refreshToken = tokenService.GetRefreshToken();

                        if(string.IsNullOrEmpty(refreshToken))
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/Login");
                        }

                        try
                        {
                            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                            var tokenResponse = await authService.RefreshToken(refreshToken);
                            tokenService.SetAccessToken(tokenResponse.AccessToken);
                            tokenService.SetRefreshToken(tokenResponse.RefreshToken);

                            context.HandleResponse();
                            context.Response.Redirect("/");
                        }
                        catch
                        {

                            context.HandleResponse();
                            context.Response.Redirect("/Login");
                        }


                    }
                };
            });


        return services;
    }
}
