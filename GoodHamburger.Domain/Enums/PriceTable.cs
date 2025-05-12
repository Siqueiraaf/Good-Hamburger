namespace GoodHamburger.Domain.Services;

using GoodHamburger.Domain.Enums;

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
        { ExtrasType.HasFries, 2.00m },
        { ExtrasType.HasSoftDrink, 2.50m }
    };
}
