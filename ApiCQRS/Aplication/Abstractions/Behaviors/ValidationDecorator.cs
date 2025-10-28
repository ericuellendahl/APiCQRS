using ApiCQRS.Domian.Interfaces.Orders;
using FluentValidation;

namespace ApiCQRS.Aplication.Abstractions.Behaviors
{
    public static class ValidationDecorator
    {
        public sealed class CommandBaseHandler<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> inner,
            IEnumerable<IValidator<TCommand>> validators) : ICommandHandler<TCommand, TResponse> where TCommand : notnull
        {
            public async Task<TResponse> HandleAsync(TCommand command)
            {
                var context = new ValidationContext<TCommand>(command);
                var validationResults = await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(context))
                );
                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();
                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
                return await inner.HandleAsync(command);
            }
        }


        public sealed class QueryBaseHandler<TQuery, TResponse>(IQueryHandler<TQuery, TResponse> inner,
            IEnumerable<IValidator<TQuery>> validators) : IQueryHandler<TQuery, TResponse> where TQuery : notnull
        {
            public async Task<TResponse> HandleAsync(TQuery query)
            {
                var context = new ValidationContext<TQuery>(query);
                var validationResults = await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(context))
                );
                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();
                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
                return await inner.HandleAsync(query);
            }
        }
    }
}
