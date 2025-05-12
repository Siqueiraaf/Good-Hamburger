using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Infrastructure.Repositories;

public class InMemoryOrderRepository
{
    private readonly List<Order> _orders = [];

    public IEnumerable<Order> GetAll() => _orders;

    public Order? GetById(Guid id) => _orders.FirstOrDefault(x => x.Id == id);

    public void Add(Order order) => _orders.Add(order);

    public void Update(Order order)
    {
        var index = _orders.FindIndex(x => x.Id == order.Id);
        if (index >= 0) _orders[index] = order;
    }

    public void Delete(Guid id)
    {
        var order = _orders.FirstOrDefault(x => x.Id == id);
        if (order is not null) _orders.Remove(order);
    }
}
