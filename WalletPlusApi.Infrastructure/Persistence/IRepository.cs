using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common;

namespace WalletPlusApi.Infrastructure.Persistence
{
    public interface IRepository<TEntity>
      where TEntity : BaseEntity
    {

        Task<long> TotalCount(Expression<Func<TEntity, bool>> where);
        Task<long> TotalCount();
        Task<List<TEntity>> GetAllPaginatedDesc(int PageNumber, int PageSize, Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetAllPaginatedUsingSkipTakeWithOrderby(int skip, int take, Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetAllPaginatedAsc(int PageNumber, int PageSize, Expression<Func<TEntity, bool>> where);

        Task<List<TEntity>> GetAllFilteredted(int skip, int take, Expression<Func<TEntity, bool>> where = null);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> Query();
        Task Add(TEntity entity);
        Task AddRange(List<TEntity> entity);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        Task Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task SoftDelete(TEntity entity);
        Task<(int id, bool IsSaved)> Save();
        void Dispose();
    }
}
