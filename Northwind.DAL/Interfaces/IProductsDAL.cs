using Northwind.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.DAL.Interfaces
{
    public interface IProductsDAL
    {
        /// <summary>取得所有產品列表
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Products>> GetAll();

        /// <summary>判斷傳入的ProductID，其產品是否存在
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> IsExist(int ProductID);

        /// <summary>依產品ID取得一筆產品
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Task<Products> GetOneByID(int ProductID);

        /// <summary>依產品ID與分類ID，取得產品列表
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        Task<IEnumerable<Products>> GetProductSByProductIDAndCategoryID(int ProductID, int CategoryID);

        /// <summary>新增一筆產品
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        Task<Products> CreateProducts(Products products);

        /// <summary>更新一筆產品
        /// 
        /// </summary>
        /// <param name="products"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Products> UpdateProducts(Products products, object key);

        /// <summary>依ProductID，刪除一筆產品
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Task<int> DeleteProducts(int ProductID);

    }
}
