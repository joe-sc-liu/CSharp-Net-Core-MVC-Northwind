using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.ViewModels;

namespace Northwind.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetAll();

        Task<CustomerViewModel> Get(string id);
    }
}
