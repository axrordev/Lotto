using System.Linq.Expressions;
using Telegram.Domain.Commons;

namespace Lotto.Data.Repositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    ValueTask<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity> UpdateAsync(TEntity entity);
    ValueTask<TEntity> DeleteAsync(TEntity entity);
    ValueTask<TEntity> DropAsync(TEntity entity);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    ValueTask<IEnumerable<TEntity>> SelectAsEnumerableAsync(
        Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
    IQueryable<TEntity> SelectAsQueryable(
        Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
}