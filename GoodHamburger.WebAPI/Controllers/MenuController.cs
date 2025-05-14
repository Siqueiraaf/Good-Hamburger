using System.Net.Mime;
using Asp.Versioning;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Enums.Sandwich;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoodHamburger.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/menu")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna o cardápio completo",
        Description = "Lista todos os tipos de sanduíches e os itens extras disponíveis com preços."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Cardápio retornado com sucesso", typeof(object))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var sandwiches = _menuService.GetSandwichMenu();
        var extras = _menuService.GetExtrasMenu();

        return Ok(new
        {
            Sandwiches = sandwiches,
            Extras = extras
        });
    }

    [HttpGet("sandwiches")]
    [SwaggerOperation(
        Summary = "Lista de sanduíches",
        Description = "Retorna todos os tipos de sanduíches disponíveis com seus respectivos preços."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista de sanduíches retornada com sucesso", typeof(Dictionary<SandwichType, decimal>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetSandwiches()
    {
        var sandwiches = _menuService.GetSandwichMenu();
        return Ok(sandwiches);
    }

    [HttpGet("extras")]
    [SwaggerOperation(
        Summary = "Lista de extras",
        Description = "Retorna os itens extras disponíveis para adicionar ao pedido com seus respectivos preços."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista de extras retornada com sucesso", typeof(Dictionary<ExtrasType, decimal>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetExtras()
    {
        var extras = _menuService.GetExtrasMenu();
        return Ok(extras);
    }
}
