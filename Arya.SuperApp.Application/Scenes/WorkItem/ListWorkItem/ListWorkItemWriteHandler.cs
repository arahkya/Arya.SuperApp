using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItem;

internal class ListWorkItemWriteHandler(
    IUnitOfWork unitOfWork,
    ILoggerFactory loggerFactory,
    IDateTimeProvider dateTimeProvider)
    : SceneReadHandler<ListWorkItemRequest, ListWorkItemResponse>(unitOfWork, loggerFactory, dateTimeProvider)
{
    protected override async Task<ListWorkItemResponse> ExecuteAsync(ListWorkItemRequest request)
    {
        var items = await UnitOfWork.Repository<WorkItemEntity>()
            .ListAsync<ListWorkItemResponseItem>(request.Page, request.PageSize, p => new ListWorkItemResponseItem(p));

        return new ListWorkItemResponse(items);
    }
}