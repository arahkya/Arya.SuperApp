namespace Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItem;

public class ListWorkItemRequest : SceneRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}