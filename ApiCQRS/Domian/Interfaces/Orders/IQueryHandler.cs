namespace ApiCQRS.Domian.Interfaces.Orders;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}
