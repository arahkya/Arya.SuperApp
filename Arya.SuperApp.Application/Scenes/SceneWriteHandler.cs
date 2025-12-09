using Arya.SuperApp.Application.Exceptions;
using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes;

public abstract class SceneWriteHandler<TRequest, TSuccessResult>(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneHandler<TRequest, TSuccessResult>(unitOfWork, loggerFactory, dateTimeProvider)
    where TRequest : SceneRequest
{
    public override async Task<SceneResult<TSuccessResult>> HandleAsync(TRequest request, Func<Exception, TSuccessResult> onError)
    {
        Log(LogLevel.Debug, request, "Start Execute Write Request");
        
        var result = new SceneResult<TSuccessResult>();
        
        try
        {
            var isValid = await ValidateRequestAsync(request);

            if (!isValid)
            {
                throw new InvalidRequestApplicationException("Validation Fail", null);
            }
            
            var executeResult = await ExecuteAsync(request);

            result.Result = executeResult;
        }
        catch (Exception e)
        {
            result.Result = onError(e);
        }
        
        Log(LogLevel.Debug, request, "End Execute Write Request");
        
        return result;
    }

    protected async Task<int> SaveSceneAsync(TRequest request)
    {
        var effected = await UnitOfWork.SaveChangesAsync(CancellationToken.None);

        if (effected > 0) 
        {
            Log(LogLevel.Debug, request, $"Request Effected {effected} Records");
        }
        else
        {
            Log(LogLevel.Warning, request, $"Failed Execute Handler");
        }
        
        return effected;
    }
}