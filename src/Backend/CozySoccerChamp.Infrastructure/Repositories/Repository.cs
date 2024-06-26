using System.Linq.Expressions;

namespace CozySoccerChamp.Infrastructure.Repositories;

public abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private DbSet<TEntity> Entities => context.Set<TEntity>();

    public virtual Task<List<TEntity>> GetAllAsync(bool tracked = true)
    {
        return GetQueryable(tracked)
            .ToListAsync();
    }

    public virtual Task<TEntity?> GetByIdAsync(int id, bool tracked = true)
    {
        return GetQueryable(tracked)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Entities.Where(expression).ToListAsync();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await context.AddAsync(entity);
        await SaveChangesAsync();

        return entity;
    }

    public async Task<List<TEntity>> CreateAsync(ICollection<TEntity> entities)
    {
        await Entities.AddRangeAsync(entities);
        await context.SaveChangesAsync();

        return entities.ToList();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Entities.Update(entity);
        await SaveChangesAsync();

        return entity;
    }

    public virtual async Task<TEntity?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id, tracked: true);

        if (entity is null)
            return null;

        Entities.Remove(entity);
        await SaveChangesAsync();

        return entity;
    }

    private IQueryable<TEntity> GetQueryable(bool tracked = true) => tracked
        ? Entities.AsQueryable()
        : Entities.AsNoTracking();

    private Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}