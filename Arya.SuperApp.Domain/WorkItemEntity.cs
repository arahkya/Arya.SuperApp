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
        if (linkedWorker.LinkedWorkItemId == Id)
        {
            throw new InvalidOperationException("Cannot link to own WorkItem");
        }

        if(!Enum.TryParse<WorkItemLinkTypes>(linkedWorker.LinkType,true , out var linkType))
        {
            throw new InvalidOperationException($"Invalid link type {linkedWorker.LinkType}");
        }
        
        LinkedWorkers.Add(linkedWorker);
    }

    public LinkedWorkItemEntity CreateDestinationLinkedWorker(LinkedWorkItemEntity sourceLinkedWorker)
    {
        return new LinkedWorkItemEntity
        {
            WorkItemId = sourceLinkedWorker.LinkedWorkItemId,
            LinkedWorkItemId = Id,
            LinkType = Enum.Parse<WorkItemLinkTypes>(sourceLinkedWorker.LinkType) switch
            {
                WorkItemLinkTypes.Parent => nameof(WorkItemLinkTypes.Child),
                WorkItemLinkTypes.Child => nameof(WorkItemLinkTypes.Parent),
                WorkItemLinkTypes.Dependent => nameof(WorkItemLinkTypes.Successor),
                _ => nameof(WorkItemLinkTypes.Related)
            },
            CreatedOn = sourceLinkedWorker.CreatedOn
        };
    }
}