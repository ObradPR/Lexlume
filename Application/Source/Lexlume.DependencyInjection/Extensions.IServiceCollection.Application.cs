using FluentValidation;
using Lexlume.Application;
using Lexlume.Application.BusinessLogic._Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Lexlume.DependencyInjection;

public static partial class Extensions
{
    private static IServiceCollection ApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(IApplicationAssemblyMarker).Assembly;

        services.AddLogging();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(cfg => { }, assembly);
        services.AddValidatorsFromAssembly(assembly);

        services.ApplicationPipelineBehaviors();

        return services;
    }

    private static IServiceCollection ApplicationPipelineBehaviors(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceMonitoringBehavior<,>));

        return services;
    }
}