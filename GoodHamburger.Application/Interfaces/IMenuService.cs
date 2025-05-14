using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Enums.Sandwich;

namespace GoodHamburger.Application.Interfaces;

public interface IMenuService
{
    Dictionary<SandwichType, decimal> GetSandwichMenu();
    Dictionary<ExtrasType, decimal> GetExtrasMenu();
}
