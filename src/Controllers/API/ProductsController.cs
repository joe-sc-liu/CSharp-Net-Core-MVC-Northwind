using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Service;
using Northwind.Service.Interfaces;

namespace CSharp_Net_Core_MVC_Northwind.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductsService _IProductsService;
        public ProductsController(IProductsService iProductsService)
        {
            _IProductsService = iProductsService;
        }


        /// <summary>相當於mvc的index   api/Products
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_IProductsService.GetAll());
        }





    }
}