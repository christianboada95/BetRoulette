using BetRoulette.Application.DataTransferObjects.Responses;
using BetRoulette.Domain.Enums;
using BetRoulette.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BetRoulette.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception.InnerException ?? context.Exception;
            HttpStatusCode statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ConflictException => HttpStatusCode.Conflict,
                ArgumentNullException => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };
            AppStatusCode errorCode = exception switch
            {
                NotFoundRouletteException => AppStatusCode.BusinessValidationError,
                ConflictOpenRouletteException => AppStatusCode.BusinessValidationError,
                ArgumentNullException => AppStatusCode.UnexpectedError,
                _ => AppStatusCode.UnexpectedError
            };

            string message = statusCode is (HttpStatusCode)500 ? "Internal Server Error" : exception.Message;
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new ObjectResult(ErrorResponse.Failure(errorCode, message));

            if (context.Exception.InnerException is not null)
                _logger.LogWarning(context.Exception.Message);
            _logger.LogError(exception, exception.Message, errorCode, statusCode);
            _logger.LogTrace(exception, exception.StackTrace);
            _logger.LogInformation("Result: {@result}", context.Result);
        }
    }
}
