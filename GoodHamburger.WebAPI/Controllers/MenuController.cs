using System.Net.Mime;
using Asp.Versioning;
using GoodHamburger.Domain.Enums;
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
    [HttpGet]
    [SwaggerOperation(
        Summary = "Retorna o cardápio completo",
        Description = "Lista todos os tipos de sanduíches e os itens extras disponíveis."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Cardápio retornado com sucesso", typeof(object))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(new
        {
            Sandwiches = Enum.GetNames(typeof(SandwichType)),
            Extras = new[] { "Fries", "SoftDrink" }
        });
    }

    [HttpGet("sandwiches")]
    [SwaggerOperation(
        Summary = "Lista de sanduíches",
        Description = "Retorna todos os tipos de sanduíches disponíveis no cardápio."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista de sanduíches retornada com sucesso", typeof(string[]))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetSandwiches() =>
        Ok(Enum.GetNames(typeof(SandwichType)));

    [HttpGet("extras")]
    [SwaggerOperation(
        Summary = "Lista de extras",
        Description = "Retorna os itens extras disponíveis para adicionar ao pedido."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista de extras retornada com sucesso", typeof(string[]))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetExtras() =>
        Ok(new[] { "Fries", "SoftDrink" });
}
