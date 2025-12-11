using Arya.SuperApp.Domain;

namespace Arya.SuperApp.Application.Scenes.WorkItem.LinkWorkItem;

public class LinkWorkItemRequest : SceneRequest
{
    public Guid WorkItemId { get; init; }
    public Guid LinkedWorkItemId { get; init; }
    public WorkItemLinkTypes LinkType { get; init; }
}