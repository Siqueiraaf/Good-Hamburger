namespace GoodHamburger.Domain.Enums.Extras;

public static class PriceTableExtras
{
    public static readonly Dictionary<ExtrasType, decimal> ExtrasPrices = new()
    {
        { ExtrasType.Fries, 2.00m },
        { ExtrasType.SoftDrink, 2.50m }
    };
}
