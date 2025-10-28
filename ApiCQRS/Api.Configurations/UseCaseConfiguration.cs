using ApiCQRS.Aplication.Abstractions.Behaviors;
using ApiCQRS.Domian.Interfaces.Orders;

namespace ApiCQRS.Api.Configurations;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationDecorator.QueryBaseHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandBaseHandler<,>));
        
        return services;
    }
}
