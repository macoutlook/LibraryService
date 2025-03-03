using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred",
            Detail = context.Exception.Message,
            Status = 500,
            Extensions = new Dictionary<string, object?>
            {
                { "traceId", context.HttpContext.TraceIdentifier }
            }
        };

        switch (context.Exception)
        {
            case IException exception:
                problemDetails.Status = exception.StatusCode;
                var customExceptionResult =
                    new ObjectResult(problemDetails);
                context.Result = customExceptionResult;
                logger.LogError(context.Exception,
                    "Exception: was thrown for request with id: {traceId}", context.HttpContext.TraceIdentifier);
                break;
            default:
                var result = new ObjectResult(problemDetails);
                context.Result = result;
                logger.LogError(context.Exception,
                    "Exception: was thrown for request with id: {traceId}", context.HttpContext.TraceIdentifier);
                break;
        }

        return Task.CompletedTask;
    }
}