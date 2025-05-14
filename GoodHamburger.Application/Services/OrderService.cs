using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Enums.Sandwich;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Services;

public class OrderService(IOrderRepository repository) : IOrderService
{
    private readonly IOrderRepository _repository = repository;

    public Order Create(OrderDto dto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Sandwich = dto.Sandwich,
            Extras = dto.Extras ?? []
        };

        _repository.Add(order);
        return order;
    }

    public Order? Update(Guid id, OrderDto dto)
    {
        var order = _repository.GetById(id);
        if (order is null) return null;

        order.Sandwich = dto.Sandwich;
        order.Extras = dto.Extras ?? [];

        _repository.Update(order);
        return order;
    }

    public IEnumerable<Order> GetAll() => _repository.GetAll();

    public void Delete(Guid id) => _repository.Delete(id);

    public decimal CalculateTotal(OrderDto dto)
    {
        if (dto == null)
            throw new OrderNullException();

        if (!Enum.IsDefined(typeof(SandwichType), dto.Sandwich))
            throw new InvalidSandwichException();

        var duplicateExtras = dto.Extras
            .GroupBy(e => e)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateExtras.Count > 0)
        {
            var extrasDuplicados = string.Join(", ", duplicateExtras);
            throw new DuplicateExtrasException(extrasDuplicados);
        }

        decimal total = dto.Sandwich switch
        {
            SandwichType.XBurguer => 5.00m,
            SandwichType.XEgg => 4.50m,
            SandwichType.XBacon => 7.00m,
            _ => throw new UndefinedSandwichException()
        };

        var fries = dto.Extras.Contains(ExtrasType.Fries);
        var softDrink = dto.Extras.Contains(ExtrasType.SoftDrink);

        if (fries) total += 2.00m;
        if (softDrink) total += 2.50m;

        if (fries && softDrink) return total * 0.8m;
        if (softDrink) return total * 0.85m;
        if (fries) return total * 0.9m;

        return total;
    }
}
