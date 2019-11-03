using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;
using Northwind.Util;


namespace Northwind.DAL
{
    public class ProductsDAL : AbstractBaseDAL, IProductsDAL
    {
        private readonly IUnitOfWork _uow;

        public ProductsDAL(IOptionsSnapshot<Settings> settings, IUnitOfWork unit) : base(settings)
        {
            _uow = unit;
        }


        public async Task<IEnumerable<Products>> GetAll()
        {
            try
            {
                return await _uow.Repository<Products>().GetAllAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsExist(int ProductID)
        {
            try
            {
                Products products = await GetOneByID(ProductID);

                if (products == null)
                    return false;
                else
                    return  true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Products> GetOneByID(int ProductID)
        {
            try
            {
                Expression<Func<Products, bool>> predicate = null;
                predicate = f => f.ProductID == ProductID;

                return await _uow.Repository<Products>().GetByAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Products>> GetProductSByProductIDAndCategoryID(int ProductID, int CategoryID)
        {
            try
            {
                Expression<Func<Products, bool>> predicate = null;
                predicate = f => f.ProductID == ProductID && f.CategoryID.Value == CategoryID;

                return await _uow.Repository<Products>().GetAllByAsync(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Products> CreateProducts(Products products)
        {
            try
            {
                return await _uow.Repository<Products>().InsertAsync(products);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Products> UpdateProducts(Products products, object key)
        {
            try
            {
                return await _uow.Repository<Products>().UpdateAsync(products, key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> DeleteProducts(int ProductID)
        {
            try
            {
                Products products = await GetOneByID(ProductID);

                if(products != null)
                {
                    return await _uow.Repository<Products>().DeleteAsync(products);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
