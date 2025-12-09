namespace Arya.SuperApp.Domain;

public class LinkedWorkItemEntity : IEntity
{
    public Guid WorkItemId { get; internal init; }
    public Guid LinkedWorkItemId { get; internal init; }
    public WorkItemEntity? WorkItem { get; internal init; }
    public WorkItemEntity? LinkedWorkItem { get; internal init; }
    public string LinkType { get; internal init; } = "Related";
    
    public Guid Id { get; internal init; }

    public string Name { get; internal init; } = string.Empty;
    
    public DateTime CreatedOn { get;  internal init; }
    
    public Guid CreatedBy { get;  internal init; }
    
    public bool IsValid()
    {
        return WorkItemId != Guid.Empty && LinkedWorkItemId != Guid.Empty && Name != string.Empty;
    }
}