namespace Arya.SuperApp.Application.Scenes.WorkItem.GetWorkItem;

public class GetWorkItemRequest : SceneRequest
{
    public Guid WorkItemId { get; set; }
}