using Application.Services;
using Infrastructure.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("FileApi", client =>
        {
            client.BaseAddress = new Uri(configuration["FileApiBaseUrl"]);
        });


        services.AddScoped<IFileService, FileApiService>();

        return services;
    }
}
