using System.Net.Mime;
using Asp.Versioning;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoodHamburger.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/orders")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    [SwaggerOperation(
        Summary = "Cria um novo pedido",
        Description = "Adiciona um novo pedido com 1 sanduíche e zero ou mais extras."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Pedido criado com sucesso", typeof(Order))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Create(OrderDto dto)
    {
        var order = _orderService.Create(dto);
        return Ok(order);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Lista todos os pedidos",
        Description = "Retorna a lista completa de pedidos cadastrados."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista de pedidos retornada com sucesso", typeof(IEnumerable<Order>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll() => Ok(_orderService.GetAll());

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
        var order = _orderService.Update(id, dto);
        if (order is null) return NotFound();
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
        _orderService.Delete(id);
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
        var total = _orderService.CalculateTotal(dto);
        return Ok(new { total });
    }
}
