namespace GoodHamburger.Domain.Enums.Sandwich;

public static class PriceTableSandwich
{
    public static readonly Dictionary<SandwichType, decimal> SandwichPrices = new()
    {
        { SandwichType.XBurguer, 5.00m },
        { SandwichType.XEgg, 4.50m },
        { SandwichType.XBacon, 7.00m }
    };
}
