using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, exception.Message);

            (string Detail, string Title, int Status) problemDetails = exception switch
            {
                InternalServerException internalServerException => 
                    (exception.Message,
                    exception.GetType().Name, 
                    StatusCodes.Status500InternalServerError),
                
                ValidationException validationException => 
                    (validationException.Message, 
                    exception.GetType().Name, 
                    StatusCodes.Status400BadRequest),

                BadRequestException badRequestException => 
                    (badRequestException.Message, 
                    exception.GetType().Name, 
                    StatusCodes.Status400BadRequest),

                NotFoundException notFoundException => 
                    (notFoundException.Message, 
                    exception.GetType().Name, 
                    StatusCodes.Status404NotFound),

                _ => (exception.Message,
                    exception.GetType().Name, 
                    StatusCodes.Status500InternalServerError)
            };

            var problemDetailsResponse = new ProblemDetails
            {
                Title = problemDetails.Title,
                Status = problemDetails.Status,
                Detail = problemDetails.Detail,
                Instance = context.Request.Path
            };

            problemDetailsResponse.Extensions.Add("traceId", context.TraceIdentifier);

            if(exception is FluentValidation.ValidationException validationEx)
            {
                problemDetailsResponse.Extensions.Add("errors", validationEx.Errors);
            }

            context.Response.StatusCode = problemDetails.Status;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetailsResponse, cancellationToken);

            return true;
        }
    }
}
