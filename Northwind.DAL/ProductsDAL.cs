using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;
using Microsoft.Extensions.Options;
using Northwind.Util;

namespace Northwind.DAL
{
    public class ProductsDAL : AbstractBaseDAL, IProductsDAL
    {
        public ProductsDAL(IOptions<Settings> settings) : base(settings)
        {

        }

        public void CreateProducts(Products products)
        {
            // Do something...
        }



    }
}
