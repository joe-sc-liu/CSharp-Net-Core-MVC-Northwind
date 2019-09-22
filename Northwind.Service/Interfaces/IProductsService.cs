using Northwind.Entities.Models;
using System.Linq;

namespace Northwind.Service.Interfaces
{
    public interface IProductsService
    {
        IQueryable<Products> GetAll();

    }



}
