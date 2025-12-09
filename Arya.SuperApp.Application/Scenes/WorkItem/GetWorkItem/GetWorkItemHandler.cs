using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.GetWorkItem;

internal class GetWorkItemHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneReadHandler<GetWorkItemRequest, WorkItemEntity?>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override async Task<WorkItemEntity?> ExecuteAsync(GetWorkItemRequest request)
    {
        var entity = await UnitOfWork.Repository<WorkItemEntity>().GetAsync(request.WorkItemId);

        if (entity is not null) return entity;
        
        Log(LogLevel.Warning, request, $"Work item not found ({request.WorkItemId})");
            
        return null;
    }
}