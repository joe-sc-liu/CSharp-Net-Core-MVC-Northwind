using Northwind.Service.Interfaces;
using Northwind.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.ViewModels;

namespace Northwind.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDAL _CustomerDAL;

        public CustomerService(ICustomerDAL iCustomerDAL)
        {
            _CustomerDAL = iCustomerDAL;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            return await _CustomerDAL.GetAll();
        }

        public async Task<CustomerViewModel> Get(string id)
        {
            return await _CustomerDAL.GetOneByID(id);
        }




    }
}
