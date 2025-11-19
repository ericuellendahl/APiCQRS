using ApiCQRS.Aplication.Abstractions.Behaviors;
using ApiCQRS.Domian.Interfaces.Orders;

namespace ApiCQRS.Api.Configurations;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        var assembly = typeof(ICommandHandler<,>).Assembly;

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationDecorator.QueryBaseHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandBaseHandler<,>));

        return services;
    }
}
