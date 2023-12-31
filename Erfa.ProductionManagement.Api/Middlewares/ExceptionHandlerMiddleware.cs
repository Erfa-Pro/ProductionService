using Erfa.ProductionManagement.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Erfa.ProductionManagement.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger { get; }

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;
            _logger.LogError(exception, exception.Message);

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new ValidationErrorDto("Invalid request", 400, validationException.ValidationErrors));
                    break;
                case EntityCreateException entityCreateException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new ErrorDto(entityCreateException.Message, 400));
                    break;
                default:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new ErrorDto("Internal server error", 500));
                    break;
            }

            _logger.LogError(result, exception);

            context.Response.StatusCode = (int)httpStatusCode;

            _logger.LogInformation($"Caught Exception {exception.GetType()} with messege: {exception.Message}");

            _logger.LogInformation($"Response: {result}, status code: {(int)httpStatusCode}");

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new ErrorDto(exception.Message, (int)httpStatusCode));
            }

            return context.Response.WriteAsync(result);
        }
    }
}
