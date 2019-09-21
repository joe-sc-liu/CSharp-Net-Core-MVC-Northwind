using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Util
{
    //請參考「https://blog.johnwu.cc/article/asp-net-core-configuration.html」
    public class Settings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string NorthwindConnection { get; set; }
    }



}
