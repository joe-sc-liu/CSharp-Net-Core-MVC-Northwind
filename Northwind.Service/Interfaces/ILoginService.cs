using Northwind.ViewModels;

namespace Northwind.Service.Interfaces
{
    public interface ILoginService
    {
        bool ValidateUser(LoginViewModel login);

    }
}
