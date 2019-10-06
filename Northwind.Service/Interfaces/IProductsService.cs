using Northwind.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Service.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<Products>> GetAll();

        Task<Products> Get(int id);



    }



}
