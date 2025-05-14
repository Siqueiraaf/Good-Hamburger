using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IOrderService
{
    Order Create(OrderDto dto);
    IEnumerable<Order> GetAll();
    Order? Update(Guid id, OrderDto dto);
    void Delete(Guid id);
    decimal CalculateTotal(OrderDto dto);
}
