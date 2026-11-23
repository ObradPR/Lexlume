using Lexlume.Domain.Interfaces;
using Lexlume.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Lexlume.DependencyInjection;

public static partial class Extensions
{
    private static IServiceCollection InfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IExceptionLogger, DbExceptionLogger>();

        return services;
    }
}