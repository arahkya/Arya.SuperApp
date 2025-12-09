namespace Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItem;

public class ListWorkItemResponse(IEnumerable<ListWorkItemResponseItem> items)
{
    public IReadOnlyCollection<ListWorkItemResponseItem> Items { get; init; } = items.ToList();
}