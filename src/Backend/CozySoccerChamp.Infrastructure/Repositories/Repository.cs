using System.Linq.Expressions;

namespace CozySoccerChamp.Infrastructure.Repositories;

public abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private DbSet<TEntity> DbSet => context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
    {
        var entity = await DbSet.FindAsync(id);

        ArgumentNullException.ThrowIfNull(entity);

        if (includes.Length == 0)
            return entity;

        foreach (var include in includes)
        {
            await context.Entry(entity).Reference(include!).LoadAsync();
        }

        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public IQueryable<TEntity> GetAllAsQueryable(bool asNoTracking = false)
    {
        return asNoTracking
            ? DbSet.AsNoTracking()
            : DbSet;
    }

    public async Task AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await DbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        DbSet.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;

        await SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        context.Set<TEntity>().UpdateRange(entities);
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity is not null)
        {
            DbSet.Remove(entity);
            await SaveChangesAsync();
        }
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> AnyAsync()
    {
        return await DbSet.AnyAsync();
    }

    private Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}