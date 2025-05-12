namespace GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Enums;

public class OrderDto
{
    public SandwichType? Sandwich { get; set; }
    public List<ExtrasType> Extras { get; set; } = [];
}
