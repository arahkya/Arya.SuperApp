using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.DeleteWorkItemLink;

internal class DeleteWorkItemLinkHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneWriteHandler<DeleteWorkItemLinkRequest, bool>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override async Task<bool> ExecuteAsync(DeleteWorkItemLinkRequest request)
    {
        await UnitOfWork.Repository<LinkedWorkItemEntity>().DeleteAsync(request.Id);
        var effectedRows = await UnitOfWork.SaveChangesAsync();
        
        return effectedRows > 0;
    }
}