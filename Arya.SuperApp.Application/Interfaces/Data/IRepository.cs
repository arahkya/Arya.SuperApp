using Arya.SuperApp.Domain;

namespace Arya.SuperApp.Application.Interfaces.Data;

public interface IRepository<TEntity> where TEntity : IEntity
{
    Task AddAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<TListItem>> ListAsync<TListItem>(int page, int pageSize, Func<TEntity, TListItem> funcItem);
    Task<TEntity?> UpdateAsync(Guid id, Action<TEntity> action);
    Task<TEntity?> GetAsync(Guid entityId);
}