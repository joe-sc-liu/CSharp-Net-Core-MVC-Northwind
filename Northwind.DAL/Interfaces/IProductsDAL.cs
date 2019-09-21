using Northwind.Entities.Models;

namespace Northwind.DAL.Interfaces
{
    public interface IProductsDAL
    {
        void CreateProducts(Products products);
    }
}
