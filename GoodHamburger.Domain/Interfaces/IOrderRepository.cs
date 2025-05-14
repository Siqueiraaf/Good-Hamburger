using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces;

public interface IOrderRepository
{
    void Add(Order order);
    void Update(Order order);
    void Delete(Guid id);
    Order? GetById(Guid id);
    IEnumerable<Order> GetAll();
}
