using System.Net;
using Microsoft.EntityFrameworkCore;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Interfaces;
using MyApp.SharedDomain.ValueObjects;

namespace MyApp.SharedDomain.Repositories
{
    public abstract class EFRepository<TEntity> : IEFRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected EFRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<PaginationResponse<TEntity>> GetAllAsync(Pagination pagination)
        {
            var results = await _dbSet.Skip(pagination.Skip).Take(pagination.Size).ToListAsync();
            return new PaginationResponse<TEntity>(results, pagination);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            return Task.Run(() => _dbSet.Update(entity));
        }

        public Task Delete(TEntity entity)
        {
            return Task.Run(() => _dbSet.Remove(entity));
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new ExceptionBase(ex.InnerException?.Message ?? ex.Message, HttpStatusCode.UnprocessableEntity);
            }
        }
    }
}
