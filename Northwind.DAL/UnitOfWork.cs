using System;
using System.Collections;
using System.Threading.Tasks;
using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;


namespace Northwind.DAL
{
    public partial class UnitOfWork : IUnitOfWork, IDisposable
    {
        //參考「https://ithelp.ithome.com.tw/articles/10157700」

        private readonly NorthwindContext _context;
        private Hashtable _repositories;

        public UnitOfWork(NorthwindContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }


}
