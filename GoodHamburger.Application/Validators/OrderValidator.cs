using FluentValidation;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Validators;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x.Sandwich)
            .NotNull()
            .WithMessage("A sandwich must be selected.");
    }
}
