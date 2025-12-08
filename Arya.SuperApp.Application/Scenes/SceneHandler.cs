using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Application.Interfaces.Scene;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes;

public abstract class SceneHandler<TRequest, TSuccessResult> : ISceneHandler<TRequest, TSuccessResult>
    where TRequest : SceneRequest
{
    protected enum LogLevel
    {
        Debug,
        Error,
        Info,
        Warning,
    }
    
    private ILogger Logger { get; set; }
    protected IUnitOfWork UnitOfWork { get; private set; }
    protected IDateTimeProvider DateTimeProvider { get; private set; }

    protected SceneHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider)
    {
        Logger = loggerFactory.CreateLogger(GetType());
        UnitOfWork = unitOfWork;
        DateTimeProvider = dateTimeProvider;
    }

    protected void Log(LogLevel logLevel, TRequest request, string message)
    { 
        switch(logLevel)
        {
            case LogLevel.Debug: Logger.LogDebug($"{request.RequestId} | {DateTimeProvider.Now} | {message}"); break;
            case LogLevel.Warning: Logger.LogWarning($"{request.RequestId} | {DateTimeProvider.Now} | {message}"); break;
            case LogLevel.Info: Logger.LogInformation($"{request.RequestId} | {DateTimeProvider.Now} | {message}"); break;
            case LogLevel.Error: Logger.LogError($"{request.RequestId} | {DateTimeProvider.Now} | {message}"); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
        };
    }
    
    protected virtual async Task<bool> ValidateRequestAsync(TRequest request)
    {
        request.UnitOfWork = UnitOfWork;
        
        var isValid = await request.IsValid(out var errors);

        if (isValid) return isValid;
        
        var validationResultMessage = string.Join(Environment.NewLine, errors.Select(x => x.ErrorMessage));

        Log(LogLevel.Warning, request, $"Validate Request Properties Invalid {validationResultMessage}");

        return isValid;
    }

    protected abstract Task<TSuccessResult> ExecuteAsync(TRequest request);

    public abstract Task<SceneResult<TSuccessResult>> HandleAsync(TRequest request, Func<Exception, TSuccessResult> onError);
}