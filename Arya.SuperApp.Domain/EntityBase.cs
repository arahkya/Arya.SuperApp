using System.ComponentModel.DataAnnotations;

namespace Arya.SuperApp.Domain;

public abstract class EntityBase
{
    public virtual bool IsValid()
    {
        var validationContext = new ValidationContext(this);
        var validationResults = new List<ValidationResult>();
        
        return Validator.TryValidateObject(this, validationContext, validationResults, true);
    }
}