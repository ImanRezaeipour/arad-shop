using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.QueryModels.Persons.Customer;

namespace Shopping.QueryService.Interfaces.Persons
{
    public interface ICustomerQueryService
    {
        ICustomerWithDefaultAddressDto GetCustomerByUserId(Guid userId);
        ICustomerWithAddressesDto Get(Guid id);
        IQueryable<CustomerDto> GetAll();
        IList<ICustomerAddressDto> GetCustomerAddressesByUserId(Guid userId);
        Task<IList<ICustomerAddressDto>> GetCustomerAddressesById(Guid id);
    }
}