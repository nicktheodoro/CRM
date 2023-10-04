using MyApp.SharedDomain.ValueObjects;

namespace MyApp.SharedDomain.Interfaces
{
    public interface IEFRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<PaginationResponse<TEntity>> GetAllAsync(Pagination pagination);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task Delete(TEntity entity);
        Task SaveChangesAsync();
    }
}
