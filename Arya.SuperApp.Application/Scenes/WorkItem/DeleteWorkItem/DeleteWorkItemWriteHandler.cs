using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.DeleteWorkItem;

internal class DeleteWorkItemWriteHandler(
    IUnitOfWork unitOfWork,
    ILoggerFactory loggerFactory,
    IDateTimeProvider dateTimeProvider)
    : SceneWriteHandler<DeleteWorkItemRequest, bool>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override Task<bool> ValidateRequestAsync(DeleteWorkItemRequest request)
    {
        var isValid = Guid.Empty != request.Id;

        if (!isValid)
        {
            Log(LogLevel.Error, request, $"Id {request.Id} is invalid");
        }
        
        return Task.FromResult(isValid);
    }

    protected override async Task<bool> ExecuteAsync(DeleteWorkItemRequest request)
    {
        var canDelete = await UnitOfWork.Repository<WorkItemEntity>().DeleteAsync(request.Id);
        
        return canDelete;
    }
}