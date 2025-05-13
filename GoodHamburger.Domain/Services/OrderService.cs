using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Services;

public class OrderService
{
    public static decimal CalculateTotal(Order order)
    {
        if (order == null)
            throw new OrderNullException();

        if (!Enum.IsDefined(typeof(SandwichType), order.Sandwich))
            throw new InvalidSandwichException();

        var duplicateExtras = order.Extras
            .GroupBy(e => e)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateExtras.Count > 0)
        {
            var extrasDuplicados = string.Join(", ", duplicateExtras);
            throw new DuplicateExtrasException(extrasDuplicados);
        }

        decimal total = order.Sandwich switch
        {
            SandwichType.XBurguer => 5.00m,
            SandwichType.XEgg => 4.50m,
            SandwichType.XBacon => 7.00m,
            _ => throw new UndefinedSandwichException()
        };

        var fries = order.Extras.Contains(ExtrasType.Fries);
        var softDrink = order.Extras.Contains(ExtrasType.SoftDrink);

        if (fries) total += 2.00m;
        if (softDrink) total += 2.50m;

        if (fries && softDrink) return total * 0.8m;
        if (softDrink) return total * 0.85m;
        if (fries) return total * 0.9m;

        return total;
    }
}
