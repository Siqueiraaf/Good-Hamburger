using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodHamburger.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers;

[ApiController]
[Route("api/menu")]
public class MenuController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new
        {
            Sandwiches = Enum.GetNames(typeof(SandwichType)),
            Extras = new[] { "Fries", "SoftDrink" }
        });
    }

    [HttpGet("sandwiches")]
    public IActionResult GetSandwiches() => Ok(Enum.GetNames(typeof(SandwichType)));

    [HttpGet("extras")]
    public IActionResult GetExtras() => Ok(new[] { "Fries", "SoftDrink" });
}
