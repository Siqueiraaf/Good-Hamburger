namespace GoodHamburger.Domain.Enums;

public static class PriceTable
{
    public static readonly Dictionary<SandwichType, decimal> SandwichPrices = new()
    {
        { SandwichType.XBurguer, 5.00m },
        { SandwichType.XEgg, 4.50m },
        { SandwichType.XBacon, 7.00m }
    };

    public static readonly Dictionary<ExtrasType, decimal> ExtrasPrices = new()
    {
        { ExtrasType.Fries, 2.00m },
        { ExtrasType.SoftDrink, 2.50m }
    };
}
