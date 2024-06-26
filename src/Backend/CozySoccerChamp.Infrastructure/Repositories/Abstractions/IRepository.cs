using System.Linq.Expressions;

namespace CozySoccerChamp.Infrastructure.Repositories.Abstractions;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(bool tracked = true);
    
    Task<TEntity?> GetByIdAsync(int id, bool tracked = true);
    Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression);

    Task<TEntity> CreateAsync(TEntity entity);
    Task<List<TEntity>> CreateAsync(ICollection<TEntity> entities);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity?> DeleteAsync(int id);
}