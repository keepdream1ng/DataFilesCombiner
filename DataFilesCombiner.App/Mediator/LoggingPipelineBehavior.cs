
using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Models;
using MediatR;
using Microsoft.Extensions.Logging;
namespace DataFilesCombiner.App.Mediator;
public class LoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger
    ): IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Logging best practices, right?
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Start of request {@RequestName}, {@Request} {@DateTimeUtc}",
                typeof(TRequest).Name,
                request,
                DateTime.UtcNow);
        }

        var result = await next();

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Completed request {@RequestName} {@Response}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result,
                DateTime.UtcNow);
        }

        return result;
    }
}
