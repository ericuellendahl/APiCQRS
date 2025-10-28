namespace ApiCQRS.Aplication.UseCase.Order.Commands;

public record CreateOrderCommand(string FirstName, string LastName, string Status, decimal TotalCost);

