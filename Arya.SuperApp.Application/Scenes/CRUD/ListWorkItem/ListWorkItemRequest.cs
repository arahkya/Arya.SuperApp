namespace Arya.SuperApp.Application.Scenes.CRUD.ListWorkItem;

public class ListWorkItemRequest : SceneRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}