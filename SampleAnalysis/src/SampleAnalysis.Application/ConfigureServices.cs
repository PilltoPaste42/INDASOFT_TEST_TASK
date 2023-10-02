namespace SampleAnalysis.Application;

using System.Reflection;

using FluentValidation;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

using SampleAnalysis.Application.Common.Behaviors;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediator(t => t.ServiceLifetime = ServiceLifetime.Transient);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        return services;
    }
}