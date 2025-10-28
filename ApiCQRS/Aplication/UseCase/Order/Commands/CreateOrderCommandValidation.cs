using FluentValidation;

namespace ApiCQRS.Aplication.UseCase.Order.Commands;

public class CreateOrderCommandValidation: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidation()
    {
        RuleFor(order => order.FirstName)
            .NotEmpty().WithMessage("FirstName name is required.");

        RuleFor(order => order.LastName)
            .NotEmpty().WithMessage("LastName name is required.");

        RuleFor(order => order.TotalCost)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
    }
}
