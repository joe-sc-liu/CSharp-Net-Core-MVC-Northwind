
using Northwind.Entities.Models;
using System.Threading.Tasks;

namespace Northwind.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;

        Task<int> SaveAsync();

        int Save();
    }


}
