using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Services;

public class OrderService
{
    public decimal CalculateTotal(Order order)
    {
        decimal total = 0;

        total += order.Sandwich switch
        {
            SandwichType.XBurguer => 5.00m,
            SandwichType.XEgg     => 4.50m,
            SandwichType.XBacon   => 7.00m,
            _ => 0
        };

        var hasFries = order.Extras.Contains(ExtrasType.HasFries);
        var hasSoda  = order.Extras.Contains(ExtrasType.HasSoftDrink);

        if (hasFries) total += 2.00m;
        if (hasSoda)  total += 2.50m;

        // Regras de desconto
        if (hasFries && hasSoda) return total * 0.8m;
        if (hasSoda)             return total * 0.85m;
        if (hasFries)            return total * 0.9m;

        return total;
    }
}
