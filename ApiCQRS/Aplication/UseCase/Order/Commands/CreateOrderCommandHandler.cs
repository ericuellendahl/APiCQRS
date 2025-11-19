using ApiCQRS.Aplication.DTOs;
using ApiCQRS.Domian.Interfaces.Orders;
using ApiCQRS.Infra.Data;
using FluentValidation;

namespace ApiCQRS.Aplication.UseCase.Order.Commands;

public class CreateOrderCommandHandler(AppDbContext context, IValidator<CreateOrderCommand> validator) : ICommandHandler<CreateOrderCommand, OrderDto>
{

    public async Task<OrderDto> HandleAsync(CreateOrderCommand command)
    {
        var order = new Domian.Models.Order
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Status = command.Status,
            CreateAt = DateTime.UtcNow,
            TotalCost = command.TotalCost
        };

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return new OrderDto
        (
            order.Id,
            order.FirstName,
            order.LastName,
            order.Status,
            order.CreateAt,
            order.TotalCost
        );
    }
}
