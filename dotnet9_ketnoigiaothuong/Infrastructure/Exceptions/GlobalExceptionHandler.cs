using dotnet9_ketnoigiaothuong.Domain.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace dotnet9_ketnoigiaothuong.Infrastructure.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message,
                StatusCode = exception is BadHttpRequestException ? 
                    (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.InternalServerError,
                Title = exception is BadHttpRequestException ? 
                    exception.GetType().Name : "Internal Server Error",
            };
            httpContext.Response.StatusCode = errorResponse.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
            return true;
        }
    }
}
