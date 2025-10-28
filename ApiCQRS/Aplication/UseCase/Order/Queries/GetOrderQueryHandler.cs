using ApiCQRS.Aplication.DTOs;
using ApiCQRS.Domian.Interfaces.Orders;
using ApiCQRS.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCQRS.Aplication.UseCase.Order.Queries;

public class GetOrderQueryHandler(AppDbContext context) : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == query.OrderId);

        return order is null ? null : new OrderDto
        (
          order.Id,
          order.FirstName,
          order.LastName,
          order.Status,
          order.CreateAt,
          order.TotalCost
        );
    }

    //public static async Task<Domian.Models.Order?> Handle(GetOrderByIdQuery query, AppDbContext context)
    //{
    //    return await context.Orders.FirstOrDefaultAsync(o => o.Id == query.OrderId);
    //}
}
