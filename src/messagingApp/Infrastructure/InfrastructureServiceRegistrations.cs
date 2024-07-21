
using Application.Services.Mail;
using Infrastructure.Services.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IMailService, SmtpEmailService>();
        services.AddOptions<SmtpConfiguration>().Configure<IConfiguration>((settings, configuration) =>
        {
            configuration.GetSection("SmtpConfiguration").Bind(settings);
        });
        return services;
    }
}
