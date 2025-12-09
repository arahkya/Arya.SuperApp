using System.ComponentModel.DataAnnotations;

namespace Arya.SuperApp.Application.Scenes.WorkItem.CreateWorkItem;

public class CreateWorkItemRequest : SceneRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;
}