namespace GoodHamburger.Domain.Services;

using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

public class OrderService
{
    public decimal CalculateTotal(Order order)
    {
        decimal total = 0;

        if (order.Sandwich.HasValue &&
            PriceTable.SandwichPrices.TryGetValue(order.Sandwich.Value, out var sandwichPrice))
        {
            total += sandwichPrice;
        }

        foreach (var extra in order.Extras)
        {
            if (PriceTable.ExtrasPrices.TryGetValue(extra, out var extraPrice))
                total += extraPrice;
        }

        return ApplyDiscount(total, order.Extras);
    }

    private decimal ApplyDiscount(decimal subtotal, List<ExtrasType> extras)
    {
        bool hasFries = extras.Contains(ExtrasType.HasFries);
        bool hasDrink = extras.Contains(ExtrasType.HasSoftDrink);

        if (hasFries && hasDrink)
            return subtotal * 0.8m;
        else if (hasDrink)
            return subtotal * 0.85m;
        else if (hasFries)
            return subtotal * 0.9m;

        return subtotal;
    }
}
