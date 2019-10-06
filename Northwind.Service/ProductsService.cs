using Northwind.Service.Interfaces;
using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsDAL _ProductsDAL;

        public ProductsService(IProductsDAL iProductsDAL)
        {
            _ProductsDAL = iProductsDAL;
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
            return await _ProductsDAL.GetAll();
        }

        public async Task<Products> Get(int id)
        {
            return await _ProductsDAL.GetOneByID(id);
        }


    }        
}
