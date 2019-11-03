using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Northwind.DAL.Interfaces;
using Northwind.Entities.Models;
using Northwind.Util;
using Northwind.ViewModels;

namespace Northwind.DAL
{
    public class CustomerDAL : AbstractBaseDAL, ICustomerDAL
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomerDAL(IOptionsSnapshot<Settings> settings, IUnitOfWork unit, IMapper mapper) : base(settings)
        {
            _uow = unit;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            try
            {
                var customer = await _uow.Repository<Customers>().GetAllAsync();

                return _mapper.Map<IEnumerable<CustomerViewModel>>(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsExist(string CustomerID)
        {
            try
            {
                CustomerViewModel customers = await GetOneByID(CustomerID);

                if (customers == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<CustomerViewModel> GetOneByID(string CustomerID)
        {
            try
            {
                Expression<Func<Customers, bool>> predicate = null;
                predicate = f => f.CustomerID == CustomerID;

                var customer = await _uow.Repository<Customers>().GetByAsync(predicate);

                return _mapper.Map<CustomerViewModel>(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerViewModel> CreateCustomers(CustomerViewModel customers)
        {
            try
            {
                var customerI = _mapper.Map<Customers>(customers);

                var insertCustomer = await _uow.Repository<Customers>().InsertAsync(customerI);

                return _mapper.Map<CustomerViewModel>(insertCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomerViewModel> UpdateCustomers(CustomerViewModel customers, object key)
        {
            try
            {
                var customerU = _mapper.Map<Customers>(customers);

                var updateCustomer = await _uow.Repository<Customers>().UpdateAsync(customerU, key);

                return _mapper.Map<CustomerViewModel>(updateCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<int> DeleteCustomers(string CustomerID)
        {
            try
            {
                var customers = await GetOneByID(CustomerID);

                var customerD = _mapper.Map<Customers>(customers);

                if (customerD != null)
                {
                    return await _uow.Repository<Customers>().DeleteAsync(customerD);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
