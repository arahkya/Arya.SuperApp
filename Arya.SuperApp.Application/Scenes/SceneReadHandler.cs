using Arya.SuperApp.Application.Exceptions;
using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Application.Interfaces.Scene;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes;

public abstract class SceneReadHandler<TRequest, TSuccessResult>(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneHandler<TRequest, TSuccessResult>(unitOfWork, loggerFactory, dateTimeProvider)
    where TRequest : SceneRequest
{
    public override async Task<SceneResult<TSuccessResult>> HandleAsync(TRequest request, Func<Exception, TSuccessResult> onError)
    {
        Log(LogLevel.Debug, request, "Start Execute Read Request");
        
        var result = new SceneResult<TSuccessResult>();
        
        try
        {
            var isValid = await ValidateRequestAsync(request);

            if (!isValid)
            {
                throw new InvalidRequestApplicationException("Validation Fail", null);
            }
            
            var executeResult = await ExecuteAsync(request);

            if (executeResult is ISceneCollectionResult collectionResult)
            {
                Log(LogLevel.Info, request, $"Read {collectionResult.Count} Items");    
            }
            
            result.Result = executeResult;
        }
        catch (Exception e)
        {
            result.Result = onError(e);
        }
        
        Log(LogLevel.Debug, request, "End Execute Read Request");
        
        return result;
    }
}