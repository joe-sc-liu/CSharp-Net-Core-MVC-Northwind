using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Net_Core_MVC_Northwind.Models
{
    //https://ithelp.ithome.com.tw/articles/10194989
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
