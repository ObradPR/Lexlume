using Microsoft.Extensions.DependencyInjection;

namespace Lexlume.DependencyInjection;

public static partial class Extensions
{
    public static IServiceCollection AllApplicationServices(this IServiceCollection services)
    {
        services.PersistenceServices();
        services.ApplicationServices();
        services.ApplicationIdentityService();
        services.InfrastructureServices();

        return services;
    }
}