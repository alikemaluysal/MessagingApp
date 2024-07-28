
using Application.Features.Auth.Rules;
using Application.Features.Chats.Rules;
using Application.Features.Messages.Rules;
using Application.Services.Auth;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistrations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<AuthBusinessRules>();
        services.AddScoped<ChatBusinessRules>();
        services.AddScoped<MessageBusinessRules>();


        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));

        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        //services.AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();



        return services;
    }
}
