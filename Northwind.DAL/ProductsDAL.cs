using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;
using Northwind.Util;


namespace Northwind.DAL
{
    public class ProductsDAL : AbstractBaseDAL, IProductsDAL
    {
        private readonly IUnitOfWork _uow;

        public ProductsDAL(IOptions<Settings> settings, IUnitOfWork unit) : base(settings)
        {
            _uow = unit;
        }

        /// <summary>取得所有產品列表
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Products> GetAll()
        {
            try
            {
                return _uow.Repository<Products>().GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>依產品ID取得一筆產品
        /// 
        /// </summary>
        /// <param name="ProductID">產品ID</param>
        /// <returns></returns>
        public Products GetOneProductByID(int ProductID)
        {
            try
            {
                Expression<Func<Products, bool>> predicate = null;
                predicate = f => f.ProductID == ProductID;

                return _uow.Repository<Products>().GetBy(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>依產品ID與分類ID，取得產品列表
        /// 
        /// </summary>
        /// <param name="ProductID">產品ID</param>
        /// <param name="CategoryID">分類ID</param>
        /// <returns></returns>
        public IQueryable<Products> GetProductSByProductIDAndCategoryID(int ProductID, int CategoryID)
        {
            try
            {
                Expression<Func<Products, bool>> predicate = null;
                predicate = f => f.ProductID == ProductID && f.CategoryID.Value == CategoryID;

                return _uow.Repository<Products>().GetBy(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CreateProducts(Products products)
        {
            try
            {
                _uow.Repository<Products>().Insert(products);

                _uow.Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProducts(Products products)
        {
            try
            {
                _uow.Repository<Products>().Update(products);

                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteProducts(Products products)
        {
            try
            {
                _uow.Repository<Products>().Delete(products);

                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
