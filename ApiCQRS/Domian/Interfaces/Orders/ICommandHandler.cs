namespace ApiCQRS.Domian.Interfaces.Orders;

public interface ICommandHandler<TCommand, TResult> where TCommand : notnull
{
    Task<TResult> HandleAsync(TCommand command);
}
