using System.ComponentModel.DataAnnotations;

namespace Arya.SuperApp.Application.Scenes.CRUD.UpdateWorkItem;

public class UpdateWorkItemRequest : SceneRequest
{
    [Required]
    public Guid Id { get; init; }
    
    [Required]
    [MaxLength(1000)]
    public required string Name { get; init; }
}