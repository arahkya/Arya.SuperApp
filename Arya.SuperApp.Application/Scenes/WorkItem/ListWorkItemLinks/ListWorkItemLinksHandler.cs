using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItemLinks;

internal class ListWorkItemLinksHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneReadHandler<ListWorkItemLinksRequest, SceneCollectionResult<LinkedWorkItemEntity>>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override async Task<SceneCollectionResult<LinkedWorkItemEntity>> ExecuteAsync(ListWorkItemLinksRequest request)
    {
        var links = await UnitOfWork.Repository<LinkedWorkItemEntity>().ListWithConditionAsync<LinkedWorkItemEntity>(i => i.WorkItemId == request.WorkItemId, p => p);
        
        return new SceneCollectionResult<LinkedWorkItemEntity>(links);
    }
}