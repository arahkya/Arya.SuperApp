using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.LinkWorkItem;

internal class LinkWorkItemHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneWriteHandler<LinkWorkItemRequest, bool>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override async Task<bool> ExecuteAsync(LinkWorkItemRequest request)
    {
        var linkedWorkItem = new LinkedWorkItemEntity
        {
            WorkItemId = request.WorkItemId,
            LinkedWorkItemId =  request.LinkedWorkItemId,
            LinkType = request.LinkType.ToString(),
            CreatedOn = DateTimeProvider.Now
        };

        var workItem = await UnitOfWork.Repository<WorkItemEntity>().GetAsync(linkedWorkItem.LinkedWorkItemId);

        if (workItem == null) return false;
        
        workItem.AddLinkedWorker(linkedWorkItem);
        
        await UnitOfWork.Repository<LinkedWorkItemEntity>().AddAsync(linkedWorkItem);
        
        var effectedRows = await UnitOfWork.SaveChangesAsync();
        
        return effectedRows > 0;
    }
}