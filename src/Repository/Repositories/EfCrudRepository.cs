using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class EfCrudRepository<TEntity, TId>(DbContext dbContext) : ICrudRepository<TEntity, TId>
    where TEntity : class
{
    private readonly DbContext _dbContext = dbContext;
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public TEntity? GetById(TId id)
    {
        return _dbSet.Find(id);
    }

    public async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public int Count()
    {
        return _dbSet.Count();
    }

    public Task<int> CountAsync()
    {
        return _dbSet.CountAsync();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    // Not a fan of delete by id. It still fetches entity to make sure it exists anyway.
    // Might mean that in the service, the work is repeated because they could do a null check first as well.
    // Might as well GetById, check for null, then delete the entity.
    public void Delete(TId id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            Delete(entity);
        }
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}