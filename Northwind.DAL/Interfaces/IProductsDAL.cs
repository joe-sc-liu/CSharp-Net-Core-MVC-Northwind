using Northwind.Entities.Models;
using System.Linq;

namespace Northwind.DAL.Interfaces
{
    public interface IProductsDAL
    {
        IQueryable<Products> GetAll();
        Products GetOneProductByID(int ProductID);
        IQueryable<Products> GetProductSByProductIDAndCategoryID(int ProductID, int CategoryID);
        void CreateProducts(Products products);
        void UpdateProducts(Products products);
        void DeleteProducts(Products products);

    }
}
