using System.ComponentModel.DataAnnotations;

namespace Arya.SuperApp.Domain;

public class WorkItemEntity : EntityBase, IEntity
{
    public Guid Id { get; init; } = Guid.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Name { get; internal set; } = string.Empty;

    public DateTime CreatedOn { get; init; }
    
    public Guid CreatedBy { get; init; }
    
    public ICollection<LinkedWorkItemEntity> LinkedWorkers { get; init; } = new List<LinkedWorkItemEntity>();

    public void AddLinkedWorker(LinkedWorkItemEntity linkedWorker)
    {
        if (linkedWorker.WorkItemId == Id)
        {
            throw new InvalidOperationException("Cannot link to own WorkItem");
        }
        
        LinkedWorkers.Add(linkedWorker);
    }
}