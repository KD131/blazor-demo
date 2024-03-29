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

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

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
}