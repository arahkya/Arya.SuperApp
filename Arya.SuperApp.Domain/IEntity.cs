namespace Arya.SuperApp.Domain;

public interface IEntity
{
    Guid Id { get; }
    string Name { get; }
    DateTime CreatedOn { get; }
    Guid CreatedBy { get; }
    bool IsValid();
}