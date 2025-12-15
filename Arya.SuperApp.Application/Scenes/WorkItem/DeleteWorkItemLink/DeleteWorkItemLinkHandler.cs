using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.DeleteWorkItemLink;

internal class DeleteWorkItemLinkHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneWriteHandler<DeleteWorkItemLinkRequest, bool>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override async Task<bool> ExecuteAsync(DeleteWorkItemLinkRequest request)
    {
        var existedLinkedWorkItem = await UnitOfWork.Repository<LinkedWorkItemEntity>().GetAsync(request.Id);
        
        if(existedLinkedWorkItem is null) return false;
        
        var relateLinks = await UnitOfWork.Repository<LinkedWorkItemEntity>()
            .ListWithConditionAsync(p => 
                (p.WorkItemId == existedLinkedWorkItem.WorkItemId && p.LinkedWorkItemId == existedLinkedWorkItem.LinkedWorkItemId) ||
                (p.WorkItemId == existedLinkedWorkItem.LinkedWorkItemId && p.LinkedWorkItemId == existedLinkedWorkItem.WorkItemId)
                , p => p);

        foreach (var linkedWorkItem in relateLinks)
        {
            await UnitOfWork.Repository<LinkedWorkItemEntity>().DeleteAsync(linkedWorkItem.Id);
        }
        
        var effectedRows = await UnitOfWork.SaveChangesAsync();
        
        return effectedRows > 0;
    }
}