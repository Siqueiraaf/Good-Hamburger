using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoodHamburger.WebAPI.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private const string DefaultErrorMessage = "Ocorreu um erro inesperado. Por favor, tente novamente.";

    public void OnException(ExceptionContext context)
    {
        var errorResponse = new
        {
            Message = DefaultErrorMessage,
            // Exception = context.Exception.Message
        };

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.ExceptionHandled = true;
    }

}
