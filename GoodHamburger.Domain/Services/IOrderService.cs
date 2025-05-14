using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Services;

public interface IOrderService
{
    decimal CalculateTotal(Order order);
}