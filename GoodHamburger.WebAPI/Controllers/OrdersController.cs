using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Services;
using GoodHamburger.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController(
    InMemoryOrderRepository repo,
    OrderService orderService) : ControllerBase
{
    private readonly InMemoryOrderRepository _repo = repo;
    private readonly OrderService _orderService = orderService;   // NEW

    // POST api/orders
    [HttpPost]
    public IActionResult Create(OrderDto dto)
    {
        var order = new Order
        {
            Sandwich = dto.Sandwich,
            Extras   = dto.Extras ?? new()
        };

        _repo.Add(order);
        return Ok(order);
    }

    // GET api/orders
    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    // PUT api/orders/{id}
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, OrderDto dto)
    {
        var order = _repo.GetById(id);
        if (order is null) return NotFound();

        order.Sandwich = dto.Sandwich;
        order.Extras   = dto.Extras ?? new();

        _repo.Update(order);
        return Ok(order);
    }

    // DELETE api/orders/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _repo.Delete(id);
        return NoContent();
    }

    // POST api/orders/total
    [HttpPost("total")]
    public IActionResult CalculateTotal(OrderDto dto)
    {
        var tempOrder = new Order
        {
            Sandwich = dto.Sandwich,
            Extras   = dto.Extras ?? new()
        };

        var total = _orderService.CalculateTotal(tempOrder);
        return Ok(new { total });
    }
}
