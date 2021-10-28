using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common;

namespace WalletPlusApi.Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity>
       where TEntity : BaseEntity
    {
        private readonly WalletPlusApiContext _dbContext;


        public Repository(WalletPlusApiContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<long> TotalCount(Expression<Func<TEntity, bool>> where)
        {
            return await _dbContext.Set<TEntity>().Where(where).CountAsync();
        }
        public async Task<long> TotalCount()
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }
        public async Task<List<TEntity>> GetAllPaginatedAsc(int PageNumber, int PageSize, Expression<Func<TEntity, bool>> where = null)
        {
            var pagedData = await _dbContext.Set<TEntity>()
                .OrderBy(t => t.CreatedAt)
                .Where(where)
               .Skip((PageNumber - 1) * PageSize)
               .Take(PageSize)

               .ToListAsync();
            return pagedData;
        }
        public async Task<List<TEntity>> GetAllPaginatedDesc(int PageNumber, int PageSize, Expression<Func<TEntity, bool>> where = null)
        {
            var pagedData = await _dbContext.Set<TEntity>()
                .OrderByDescending(t => t.CreatedAt)
                .Where(where)
               .Skip((PageNumber - 1) * PageSize)
               .Take(PageSize)

               .ToListAsync();
            return pagedData;
        }
        public async Task<List<TEntity>> GetAllPaginatedUsingSkipTakeWithOrderby(int skip, int take, Expression<Func<TEntity, bool>> where)
        {
            var pagedData = await _dbContext.Set<TEntity>()
                .OrderByDescending(t => t.CreatedAt)
               .Where(where)
               .Skip(skip)
               .Take(take)
              .ToListAsync();
            return pagedData;
        }

        public async Task<List<TEntity>> GetAllFilteredted(int skip, int take,
            Expression<Func<TEntity, bool>> where = null)
        {
            var pagedData = await _dbContext.Set<TEntity>()
                .Where(where)
               .Skip(skip)
               .Take(take)
               .ToListAsync();
            return pagedData;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> where)
        {
            return await _dbContext.Set<TEntity>().Where(a => a.IsDeleted != true).FirstOrDefaultAsync(where);
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> where)
        {
            return await _dbContext.Set<TEntity>()
                .Where(where).ToListAsync();
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRange(List<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }
        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().Where(a => a.IsDeleted == false)
                    .OrderByDescending(a => a.CreatedAt);
        }

        public void Update(TEntity entity)
        {
            entity.LastUpdated = DateTime.Now;
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return _dbContext.Set<TEntity>().Where(a => a.IsDeleted == false)
                .Where(where)
                .OrderByDescending(a => a.CreatedAt);
        }

        public async Task SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            _dbContext.Set<TEntity>().Update(entity);
        }
        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync(default) >= 0;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            entities.ForEach(x => x.LastUpdated = DateTime.Now);
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }


    }
}
