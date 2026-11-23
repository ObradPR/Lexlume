using Lexlume.Common;
using Lexlume.Domain.Interfaces;
using Lexlume.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lexlume.DependencyInjection;

public static partial class Extensions
{
    private static IServiceCollection PersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(
            opt => opt.UseSqlServer(Settings.ConnectionString));

        services.AddScoped<IDatabaseContext, ApplicationDbContext>();

        return services;
    }
}