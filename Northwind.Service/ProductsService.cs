using Northwind.Service.Interfaces;
using Northwind.DAL;
using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;
using System.Linq;

namespace Northwind.Service
{
    public class ProductsService : IProductsService
    {
        private IProductsDAL _ProductsDAL;
        public ProductsService(IProductsDAL iProductsDAL)
        {
            _ProductsDAL = iProductsDAL;
        }

        public IQueryable<Products> GetAll()
        {
            return _ProductsDAL.GetAll();
        }

        public Products Get(int id)
        {
            return _ProductsDAL.GetOneProductByID(id);
        }


    }        
}
