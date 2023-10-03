using Ballast.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ballast.Api.Filters;

public class BusinessExceptionAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is InvalidPasswordException)
        {
            context.Result = new UnauthorizedResult();
            context.ExceptionHandled = true;
        }
        else if (context.Exception is NotFoundException)
        {
            context.Result = new NotFoundResult();
            context.ExceptionHandled = true;
        }
        else if (context.Exception is Ballast.Application.Exceptions.ValidationException validationException)
        {
            context.Result = new BadRequestObjectResult(validationException.Errors);
            context.ExceptionHandled = true;
        }
    }
}