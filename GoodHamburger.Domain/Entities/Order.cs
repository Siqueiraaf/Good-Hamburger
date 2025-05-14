using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Enums.Sandwich;

namespace GoodHamburger.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public SandwichType Sandwich { get; set; }
    public List<ExtrasType> Extras { get; set; } = [];
}
