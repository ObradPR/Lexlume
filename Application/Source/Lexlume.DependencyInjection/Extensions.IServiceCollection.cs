using FluentValidation;
using Lexlume.Application;
using Lexlume.Application.BusinessLogic._Behaviors;
using Lexlume.Common;
using Lexlume.Domain.Interfaces;
using Lexlume.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lexlume.DependencyInjection;

public static partial class Extensions
{
    public static IServiceCollection AllApplicationServices(this IServiceCollection services)
    {
        PersistenceServices(services);
        ApplicationServices(services);
        ApplicationIdentityService(services);
        //InfrastructureService(services);

        return services;
    }

    private static IServiceCollection PersistenceServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(
            opt => opt.UseSqlServer(Settings.ConnectionString));

        services.AddScoped<IDatabaseContext, ApplicationDbContext>();

        return services;
    }

    private static IServiceCollection ApplicationServices(IServiceCollection services)
    {
        var assembly = typeof(IApplicationAssemblyMarker).Assembly;

        services.AddLogging();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(cfg => { }, assembly);
        services.AddValidatorsFromAssembly(assembly);

        ApplicationPipelineBehaviors(services);

        return services;
    }

    private static IServiceCollection ApplicationIdentityService(IServiceCollection services)
    {
        return services;
    }

    //private static IServiceCollection InfrastructureServices(IServiceCollection services)
    //{
    //    services.AddTransient<IExceptionLogger, DbExceptionLogger>();

    //    return services;
    //}

    private static IServiceCollection ApplicationPipelineBehaviors(IServiceCollection services)
    {
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceMonitoringBehavior<,>));

        return services;
    }
}