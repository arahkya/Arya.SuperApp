using System.ComponentModel.DataAnnotations;
using Arya.SuperApp.Application.Interfaces.Data;

namespace Arya.SuperApp.Application.Scenes;

public abstract class SceneRequest
{
    public required string RequestId { get; init; } = string.Empty;
    
    protected internal IUnitOfWork? UnitOfWork { get; internal set; }
    
    public virtual Task<bool> IsValid(out IEnumerable<ValidationResult> validateResults)
    {
        validateResults = [];
        
        var validationContext = new ValidationContext(this);
        var validationResults = new List<ValidationResult>();
        
        var isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);
        
        validateResults = validationResults;
        
        return Task.FromResult(isValid);
    }
}