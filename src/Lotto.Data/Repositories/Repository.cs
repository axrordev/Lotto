using Lotto.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Telegram.Domain.Commons;

namespace Lotto.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext context;
    private readonly DbSet<TEntity> set;
    public Repository(AppDbContext context)
    {
        this.context = context;
        this.set = context.Set<TEntity>();
    }

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        return (await set.AddAsync(entity)).Entity;
    }

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    public async ValueTask<TEntity> DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    public async ValueTask<TEntity> DropAsync(TEntity entity)
    {
        return await Task.FromResult(set.Remove(entity).Entity);
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
    {
        var query = set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync();
    }

    public async ValueTask<IEnumerable<TEntity>> SelectAsEnumerableAsync(
        Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return await query.ToListAsync();
    }

    public IQueryable<TEntity> SelectAsQueryable(
        Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return query;
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}

