using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.QueryModels.Persons.Customer;
using Shopping.QueryService.Interfaces.Persons;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Persons
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly IReadOnlyRepository<Customer, Guid> _repository;
        public CustomerQueryService(IReadOnlyRepository<Customer, Guid> repository)
        {
            _repository = repository;
        }
        public ICustomerWithDefaultAddressDto GetCustomerByUserId(Guid userId)
        {
            var customer = _repository.AsQuery().SingleOrDefault(item => item.UserId == userId);
            return customer.ToCustomerWithDefaultAddressDto();
        }

        public ICustomerWithAddressesDto Get(Guid id)
        {
            var customer = _repository.Find(id);
            return customer.ToCustomerWithAddressesDto();
        }

        public IQueryable<CustomerDto> GetAll()
        {
            var result = _repository.AsQuery()
                .OrderByDescending(p => p.RegisterDate).Select(item => new CustomerDto
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    IsActive = item.IsActive,
                    EmailAddress = item.EmailAddress,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MobileNumber = item.MobileNumber,
                    CreationTime = item.RegisterDate,
                    PersonNumber = item.PersonNumber
                });
            return result;
        }
        public IList<ICustomerAddressDto> GetCustomerAddressesByUserId(Guid userId)
        {
            var customer = _repository.AsQuery().SingleOrDefault(item => item.UserId == userId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            return customer.CustomerAddresses.ToList().Select(item => item.ToDto()).ToList();
        }
        public async Task<IList<ICustomerAddressDto>> GetCustomerAddressesById(Guid id)
        {
            var customer = await _repository.FindAsync(id);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            return customer.CustomerAddresses.ToList().Select(item => item.ToDto()).ToList();
        }
    }
}