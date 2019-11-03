using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Util
{
    //請參考「https://blog.johnwu.cc/article/asp-net-core-configuration.html」
    //參考「https://itweihan.azurewebsites.net/blog/10200191」改用IOptionsSnapshot注入，原因在IOptionsSnapshot提供OnChage事件，監聽配置文件:"當文件有變動，系統資料也會跟著變動"
    public class Settings
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public Tokens Tokens { get; set; }
    }

    public class ConnectionStrings
    {
        public string NorthwindConnection { get; set; }
    }

    public class Tokens
    {
        public string ValidIssuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public int ValidExpires { get; set; }
    }


}
