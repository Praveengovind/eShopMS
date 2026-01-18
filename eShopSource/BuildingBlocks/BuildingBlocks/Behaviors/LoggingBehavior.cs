using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handling {RequestName} with payload: {@Request} , Response received: {@Response}",
                typeof(TRequest).Name, request, typeof(TResponse).Name);

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            var response = await next();
            timer.Stop();

            var duration = timer.Elapsed;

            logger.LogInformation("[PERFORMANCE] Time Tasken for the request {@TRequest} is {@Duration}", request, duration);

            return await next();
        }
    }
}
