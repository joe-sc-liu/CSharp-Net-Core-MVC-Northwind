using System;
using CSharp_Net_Core_MVC_Northwind.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Northwind.Service.Interfaces;
using Northwind.Util;
using Northwind.ViewModels;

//https://blog.miniasp.com/post/2019/10/13/How-to-use-JWT-token-based-auth-in-aspnet-core-22

namespace CSharp_Net_Core_MVC_Northwind.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IOptionsSnapshot<Settings> _settings;
        private readonly ILoginService _ILoginService;

        public TokenController(Serilog.ILogger logger, IOptionsSnapshot<Settings> settings, ILoginService iLoginService)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            else
                _logger = logger.ForContext<TokenController>();

            _settings = settings;

            _ILoginService = iLoginService;
        }

        [HttpPost]
        public ActionResult<ResultModel> SignIn([FromBody] LoginViewModel login)
        {
            //https://blog.johnwu.cc/article/ironman-day09-asp-net-core-model-binding.html
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                _logger.Debug("登入驗證，並取得token");                   

                var issuer = _settings.Value.Tokens.ValidIssuer;
                var signKey = _settings.Value.Tokens.IssuerSigningKey; // 請換成至少 16 字元以上的安全亂碼
                var expires = _settings.Value.Tokens.ValidExpires; // 單位: 分鐘

                if (_ILoginService.ValidateUser(login))
                {
                    //List相當於mvc的index  api/Customers
                    var result = new ResultModel
                    {
                        Data = JwtHelpers.GenerateToken(issuer, signKey, login.Username, expires),
                        IsSuccess = true
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ResultModel { IsSuccess = false, Message = "失敗" });
                }                
            }
            catch (Exception e)
            {
                _logger.Error(e, "");
                return BadRequest(new ResultModel { IsSuccess = false, Message = "" });
                throw;
            }
        }






    }
}
