using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.DAL
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private NorthwindContext context;
        private DbSet<TEntity> TableSet;

        public GenericRepository()
        {
             context = new NorthwindContext();
             TableSet = context.Set<TEntity>();
        }
        public GenericRepository(NorthwindContext context1)
        {
             context = context1;
             TableSet = context.Set<TEntity>();
        }


        /// <summary>取得所有資料</summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {

            IQueryable<TEntity> query = TableSet;
            return query;
        }

        /// <summary>取得傳入條件資訊</summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {

            IQueryable<TEntity> query = TableSet.Where(predicate);
            return query;
        }

        /// <summary>新增一筆資料</summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            TableSet.Add(entity);
        }

        /// <summary>修改資料</summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            TableSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }


        /// <summary>刪除某筆資料 by Entity</summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            //假如Entity處於Detached狀態，就先Attach起來，這樣才能順利移除
            if (context.Entry(entity).State == EntityState.Detached)
            {
                TableSet.Attach(entity);
            }
            TableSet.Remove(entity);
        }

        public virtual int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<TEntity> SelectPaged(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>,
          IOrderedQueryable<TEntity>> orderBy = null,
          List<Expression<Func<TEntity, object>>> includes = null,
          int? page = null,
          int? pageSize = null)
        {
            IQueryable<TEntity> query = TableSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        public async Task<IEnumerable<TEntity>> SelectPagedAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          List<Expression<Func<TEntity, object>>> includes = null,
          int? page = null,
          int? pageSize = null)
        {
            return await SelectPaged(filter, orderBy, includes, page, pageSize).ToListAsync();
        }


        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await TableSet.ToListAsync();
        }

        public async Task<TEntity> GetAsync(object id)
        {
            return await TableSet.FindAsync(id);
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> match)
        {
            return await TableSet.SingleOrDefaultAsync(match);
        }

        public async Task<ICollection<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> match)
        {
            return await TableSet.Where(match).ToListAsync();
        }


        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            TableSet.Add(entity);

            await SaveAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, object key)
        {
            if (entity == null)
                return null;

            TEntity existing = await GetAsync(key);
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(entity);

                await SaveAsync();
            }
            return existing;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            //假如Entity處於Detached狀態，就先Attach起來，這樣才能順利移除
            if (context.Entry(entity).State == EntityState.Detached)
            {
                TableSet.Attach(entity);
            }
            TableSet.Remove(entity);

            return await SaveAsync();
        }

        public async Task<int> CountAsync()
        {
            return await TableSet.CountAsync();
        }


        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }




        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
