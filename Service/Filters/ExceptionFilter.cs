using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Service.Filters;

public class ExceptionFilter : IAsyncExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case IException:
                var exception = (IException) context.Exception;
                var customExceptionResult =
                    new ObjectResult($"{exception.OutsideMessage}{Environment.NewLine}Find details in log");
                customExceptionResult.StatusCode = exception.StatusCode;
                context.Result = customExceptionResult;
                _logger.LogError(context.Exception,
                    $"Exception: {context.Exception.Message}{Environment.NewLine}{context.Exception.InnerException?.Message}");
                break;
            default:
                var result = new ObjectResult(context.Exception.Message);
                result.StatusCode = 500;
                context.Result = result;
                _logger.LogError(context.Exception,
                    $"Exception: {context.Exception.Message}{Environment.NewLine}{context.Exception.InnerException?.Message}");
                break;
        }
    }
}