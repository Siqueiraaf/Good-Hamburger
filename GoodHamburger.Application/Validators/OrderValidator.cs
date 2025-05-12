using FluentValidation;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Enums;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x.Sandwich)
            .NotNull().WithMessage("A sandwich must be selected.");

        RuleFor(x => x.Extras)
            .Must(BeDistinct).WithMessage("Extras cannot contain duplicates.");
    }

    private bool BeDistinct(List<ExtrasType>? extras)
    {
        return extras == null || extras.Distinct().Count() == extras.Count;
    }
}
