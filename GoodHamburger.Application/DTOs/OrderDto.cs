using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.DTOs;

public class OrderDto
{
    public SandwichType Sandwich { get; set; }
    public List<ExtrasType> Extras { get; set; } = [];
}
