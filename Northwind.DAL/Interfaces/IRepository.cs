using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.DAL.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);
        void Update(TEntity entity);

        void Delete(TEntity entity);

        int Save();

        IQueryable<TEntity> SelectPaged(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           int? page = null,
           int? pageSize = null);

        Task<IEnumerable<TEntity>> SelectPagedAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           int? page = null,
           int? pageSize = null);

        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(object id);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity> InsertAsync(TEntity t);
        Task<TEntity> UpdateAsync(TEntity updated, object key);
        Task<int> DeleteAsync(TEntity t);
        Task<int> CountAsync();

        Task<int> SaveAsync();


    }
}
