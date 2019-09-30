using System;
using CSharp_Net_Core_MVC_Northwind.Models;
using Microsoft.AspNetCore.Mvc;
using Northwind.Service.Interfaces;


namespace CSharp_Net_Core_MVC_Northwind.Controllers.API
{
    //參考https://ithelp.ithome.com.tw/articles/10194989

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductsService _IProductsService;
        private readonly Serilog.ILogger _logger;

        public ProductsController(IProductsService iProductsService, Serilog.ILogger logger)
        {
            _IProductsService = iProductsService;

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            else
                _logger = logger.ForContext<ProductsController>();
        }

        

        /// <summary>取得所有的Product資料
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel List()
        {
            //api/Products
            try
            {
                _logger.Debug("取得產品列表");

                //List相當於mvc的index  api/Products
                var result = new ResultModel
                {
                    Data = _IProductsService.GetAll(),
                    IsSuccess = true
                };

                return result;
            } 
            catch(Exception e)
            {
                _logger.Error(e,"");
                return new ResultModel { IsSuccess = false, Message = "" };
                throw;
            }
        }

        /// <summary>取得傳入編號，產品詳細資訊
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ResultModel Get(int id)
        {
            //api/Products/75

            try
            {
                _logger.Debug("取得產品資訊：{id}",id);

                var result = new ResultModel
                {
                    Data = _IProductsService.Get(id),
                    IsSuccess = true
                };

                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "");
                return new ResultModel { IsSuccess = false, Message = "" };
                throw;
            }
        }







    }
}