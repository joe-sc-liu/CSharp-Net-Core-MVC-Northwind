using Northwind.Service.Interfaces;
using Northwind.ViewModels;

namespace Northwind.Service
{
    public class LoginService : ILoginService
    {
        public bool ValidateUser(LoginViewModel login)
        {
            return true; // TODO 寫驗證帳號fun
        }



    }
}
