namespace Repository.Repositories;

public interface ICrudRepository<TEntity, TId>
{
    TEntity? GetById(TId id);
    IEnumerable<TEntity> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void Delete(TId id);
    void Save();
}