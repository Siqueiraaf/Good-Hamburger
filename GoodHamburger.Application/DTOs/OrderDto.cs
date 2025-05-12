namespace GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Enums;

public class OrderDto
{
    public SandwichType? Sandwich { get; set; }
    public bool HasFries { get; set; }
    public bool HasSoftDrink { get; set; }
}
