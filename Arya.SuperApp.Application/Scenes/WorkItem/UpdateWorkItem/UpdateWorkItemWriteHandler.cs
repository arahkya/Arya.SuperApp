using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.UpdateWorkItem;

internal class UpdateWorkItemWriteHandler(
    IUnitOfWork unitOfWork,
    ILoggerFactory loggerFactory,
    IDateTimeProvider dateTimeProvider) : SceneWriteHandler<UpdateWorkItemRequest, bool>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override async Task<bool> ExecuteAsync(UpdateWorkItemRequest request)
    {
        var entity = await UnitOfWork.Repository<WorkItemEntity>().UpdateAsync(request.Id, p =>
        {
            p.Name = request.Name;
        });

        if (entity?.IsValid() ?? true)
        {
            var effectedRows = await SaveSceneAsync(request);
            
            return effectedRows > 0;
        }
        
        Log(LogLevel.Error, request, $"Entity is not valid");
            
        return false;

    }
}