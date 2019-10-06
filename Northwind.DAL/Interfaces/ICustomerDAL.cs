using Northwind.Entities.Models;
using Northwind.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Northwind.DAL.Interfaces
{
    public interface ICustomerDAL
    {

        Task<IEnumerable<CustomerViewModel>> GetAll();

        Task<bool> IsExist(string CustomerID);

        Task<CustomerViewModel> GetOneByID(string CustomerID);

        Task<CustomerViewModel> CreateCustomers(CustomerViewModel customers);

        Task<CustomerViewModel> UpdateCustomers(CustomerViewModel customers, object key);

        Task<int> DeleteCustomers(string CustomerID);

    }
}
