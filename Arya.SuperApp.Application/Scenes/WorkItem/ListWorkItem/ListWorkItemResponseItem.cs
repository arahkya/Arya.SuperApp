using Arya.SuperApp.Domain;

namespace Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItem;

public class ListWorkItemResponseItem(WorkItemEntity entity)
{
    public Guid Id { get; private set; } = entity.Id;
    public string Name { get; private set; } = entity.Name;
}