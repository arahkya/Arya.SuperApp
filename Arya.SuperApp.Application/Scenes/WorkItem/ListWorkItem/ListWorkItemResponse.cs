namespace Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItem;

public class ListWorkItemResponse(IEnumerable<ListWorkItemResponseItem> items) : SceneCollectionResult<ListWorkItemResponseItem>(items)
{
    
}