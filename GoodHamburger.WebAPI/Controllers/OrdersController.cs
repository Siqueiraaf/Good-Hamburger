using System.Net.Mime;
using Asp.Versioning;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Services;
using GoodHamburger.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoodHamburger.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/orders")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OrdersController(
    InMemoryOrderRepository repository) : ControllerBase
{
    private readonly InMemoryOrderRepository _repository = repository;

    [HttpPost]
    [SwaggerOperation(
        Summary = "Cria um novo pedido",
        Description = "Adiciona um novo pedido com 1 sanduíche e zero ou mais extras."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Pedido criado com sucesso", typeof(Order))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Create(OrderDto dto)
    {
        var order = new Order
        {
            Sandwich = dto.Sandwich,
            Extras = dto.Extras ?? []
        };

        _repository.Add(order);
        return Ok(order);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Lista todos os pedidos",
        Description = "Retorna a lista completa de pedidos cadastrados."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista de pedidos retornada com sucesso", typeof(IEnumerable<Order>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(_repository.GetAll());

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Atualiza um pedido",
        Description = "Atualiza o sanduíche e os extras de um pedido existente pelo seu ID."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Pedido atualizado com sucesso", typeof(Order))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido não encontrado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid id, OrderDto dto)
    {
        var order = _repository.GetById(id);
        if (order is null) return NotFound();

        order.Sandwich = dto.Sandwich;
        order.Extras = dto.Extras ?? [];

        _repository.Update(order);
        return Ok(order);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Remove um pedido",
        Description = "Exclui um pedido com base no ID informado."
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Pedido removido com sucesso")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(Guid id)
    {
        _repository.Delete(id);
        return NoContent();
    }

    [HttpPost("total")]
    [SwaggerOperation(
        Summary = "Calcula o total de um pedido",
        Description = "Calcula o preço total com base no sanduíche e extras informados, aplicando descontos se houver."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Total calculado com sucesso", typeof(decimal))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CalculateTotal(OrderDto dto)
    {
        var tempOrder = new Order
        {
            Sandwich = dto.Sandwich,
            Extras = dto.Extras ?? []
        };

        var total = OrderService.CalculateTotal(tempOrder);
        return Ok(new { total });
    }
}
