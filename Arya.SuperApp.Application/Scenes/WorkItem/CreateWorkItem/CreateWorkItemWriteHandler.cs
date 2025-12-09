using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Domain;
using Microsoft.Extensions.Logging;

namespace Arya.SuperApp.Application.Scenes.WorkItem.CreateWorkItem;

internal class CreateWorkItemWriteHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IDateTimeProvider dateTimeProvider) : SceneWriteHandler<CreateWorkItemRequest, Guid>(unitOfWork, loggerFactory, dateTimeProvider)
{   
    protected override async Task<Guid> ExecuteAsync(CreateWorkItemRequest request)
    {
        var guid = Guid.CreateVersion7();

        var entity = new WorkItemEntity
        {
            Id = guid,
            Name = request.Name,
            CreatedOn = DateTimeProvider.Now,
            CreatedBy = Guid.Empty
        };

        if (!entity.IsValid())
        {
            Log(LogLevel.Error, request, $"Entity is not valid");
            
            return Guid.Empty;
        }
        
        await UnitOfWork.Repository<WorkItemEntity>().AddAsync(entity);
        
        Log(LogLevel.Info, request, $"WorkItem Created ({guid})");
        
        return guid;
    }
}