using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repository;

public class EfRepositoryBase<TEntity>(DbContext context) : IRepository<TEntity>, IAsyncRepository<TEntity>, IQuery<TEntity>
    where TEntity : class, new()
{
    public IQueryable<TEntity> Query() => context.Set<TEntity>().AsQueryable();

    #region Sync
    public TEntity Add(TEntity entity)
    {
        context.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        context.Update(entity);
        context.SaveChanges();
        return entity;
    }
    public TEntity Delete(TEntity entity)
    {
        context.Remove(entity);
        context.SaveChanges();
        return entity;
    }

    public TEntity? Get(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool tracking = false)
    {
        var query = Query();

        if (!tracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        return query.FirstOrDefault(predicate);

    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedEnumerable<TEntity>>? orderBy = null,
        bool tracking = false)
    {
        var query = Query();

        if (!tracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            query = orderBy(query).AsQueryable();

        return query.ToList();


    }

    public bool Any(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Query();

        if (include is not null)
            query = include(query);

        return query.Any(predicate);
    }

    #endregion

    #region Async   

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        context.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool tracking = false)
    {
        var query = Query();

        if (!tracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedEnumerable<TEntity>>? orderBy = null, bool tracking = false)
    {
        var query = Query();

        if (!tracking)
            query = query.AsNoTracking();

        if (include is not null)
            query = include(query);

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            query = orderBy(query).AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Query();

        if (include is not null)
            query = include(query);

        return await query.AnyAsync(predicate);
    }
    #endregion

}


