using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Enums.Extras;
using GoodHamburger.Domain.Enums.Sandwich;

namespace GoodHamburger.Application.Services;

public class MenuService : IMenuService
{
    public Dictionary<SandwichType, decimal> GetSandwichMenu()
    {
        return PriceTableSandwich.SandwichPrices;
    }

    public Dictionary<ExtrasType, decimal> GetExtrasMenu()
    {
        return PriceTableExtras.ExtrasPrices;
    }
}
