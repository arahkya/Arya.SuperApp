using Arya.SuperApp.Domain;

namespace Arya.SuperApp.Application.Interfaces.Data;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}