using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Arya.SuperApp.Infrastructure;

public class SuperAppDbContext(DbContextOptions<SuperAppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<WorkItemEntity> WorkItems { get; set; }
    
    internal class GenericRepository<TEntity>(SuperAppDbContext dbContext) : IRepository<TEntity> 
        where TEntity : class, IEntity
    {
        public async Task AddAsync(TEntity entity)
        {
            await dbContext.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await dbContext.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == id);
        
            if (entity == null)
            {
                return false;
            }
        
            dbContext.Remove(entity);
        
            return true;
        }

        public async Task<IEnumerable<TListItem>> ListAsync<TListItem>(int page, int pageSize, Func<TEntity, TListItem> funcItem)
        {
            var entities = await dbContext.Set<TEntity>().Skip(page * pageSize).Take(pageSize).ToListAsync();
        
            return entities.Select(funcItem);
        }

        public async Task<TEntity?> UpdateAsync(Guid id, Action<TEntity> action)
        {
            var entity = await dbContext.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == id);
        
            if (entity == null)
            {
                return null;
            }
        
            action(entity);
        
            return entity;
        }

        public async Task<TEntity?> GetAsync(Guid entityId)
        {
            var entity = await dbContext.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == entityId);
            
            return entity;
        }
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
    {
        return new GenericRepository<TEntity>(this);
    }
}