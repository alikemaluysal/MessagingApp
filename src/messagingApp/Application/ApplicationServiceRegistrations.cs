
using Application.Features.Auth.Rules;
using Application.Features.Chats.Rules;
using Application.Features.Messages.Rules;
using Application.Services.Auth;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        services.AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
