using AccraCityApi.Contracts.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace AccraCityApi.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var response = new FinalResponse<object>
        {
            StatusCode = 500,
            Message = "An unexpected error occurred. Please try again later."
        };
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}
