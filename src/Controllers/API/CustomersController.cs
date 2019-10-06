using System;
using CSharp_Net_Core_MVC_Northwind.Models;
using Microsoft.AspNetCore.Mvc;
using Northwind.Service.Interfaces;

namespace CSharp_Net_Core_MVC_Northwind.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _ICustomerService;
        private readonly Serilog.ILogger _logger;

        public CustomersController(ICustomerService iCustomerService, Serilog.ILogger logger)
        {
            _ICustomerService = iCustomerService;

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            else
                _logger = logger.ForContext<CustomersController>();
        }


        /// <summary>取得所有客戶資訊
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel List()
        {
            //api/Customers
            try
            {
                _logger.Debug("取得客戶列表");

                //List相當於mvc的index  api/Customers
                var result = new ResultModel
                {
                    Data = _ICustomerService.GetAll().Result,
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

        /// <summary>取得傳入編號，客戶詳細資訊
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ResultModel Get(string id)
        {
            //api/Customers/ERNSH

            try
            {
                _logger.Debug("取得客戶資訊：{id}", id);

                var c = _ICustomerService.Get(id);
                c.Result.Fax2 = "abc";

                var result = new ResultModel
                {
                    Data = c.Result,
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