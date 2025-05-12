namespace GoodHamburger.Domain.Entities;

using GoodHamburger.Domain.Enums;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public SandwichType? Sandwich { get; set; }
    public List<ExtrasType> Extras { get; set; } = new();
}
