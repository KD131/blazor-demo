namespace Repository.Repositories;

public interface ICrudRepository<TEntity, TId>
{
    TEntity? GetById(TId id);
    Task<TEntity?> GetByIdAsync(TId id);
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync();
    int Count();
    Task<int> CountAsync();
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void Delete(TId id);
    void Save();
    Task SaveAsync();
}