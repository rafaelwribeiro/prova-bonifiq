using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository.Interface;
using System;
using System.Linq.Expressions;

namespace ProvaPub.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TestDbContext _db;

    public Repository(TestDbContext db)
    {
        _db = db;
    }

    public async Task<T> Add(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<T> Delete(int id)
    {
        var entity = await _db.Set<T>().FindAsync(id);
        if (entity is null)
            throw new InvalidOperationException("entity not found");

        _db.Set<T>().Remove(entity);
        return entity;
    }

    public async Task<IList<T>> GetAll()
    {
        return await _db.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetByFilter(Expression<Func<T, bool>> filter)
    {
        return await _db.Set<T>().FirstOrDefaultAsync(filter);
    }

    public async Task<IList<T>> GetManyByFilter(Expression<Func<T, bool>> filter)
    {
        return await _db.Set<T>().AsNoTracking().Where(filter).ToListAsync();
    }

    public async Task<bool> AnyByFilter(Expression<Func<T, bool>> filter)
    {
        return await _db.Set<T>().AsNoTracking().Where(filter).AnyAsync();
    }

    public void Update(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        _db.Set<T>().Update(entity);
    }

    public async Task<IList<T>> GetManyByFilterPaginated(Expression<Func<T, bool>> filter, int pageNumber, int pageSize)
    {
        return await _db.Set<T>().AsNoTracking().Skip((pageNumber-1) * pageSize).Take(pageSize).Where(filter).ToListAsync();
    }

    public async Task<IList<T>> GetManyPaginated(int pageNumber, int pageSize)
    {
        return await _db.Set<T>().AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
    {
        return await _db.Set<T>().CountAsync(filter);
    }
}