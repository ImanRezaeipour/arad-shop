using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Promoters.Aggregates;
using Shopping.DomainModel.Aggregates.Promoters.Interfaces;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Promoters.Services
{
    public class PromoterDomainService : IPromoterDomainService
    {
        private readonly IRepository<Promoter> _repository;
        public PromoterDomainService(IRepository<Promoter> repository)
        {
            _repository = repository;
        }
        public async Task CheckPromoterIsExist(string nationalCode)
        {
            if (await _repository.AsQuery().AnyAsync(p => p.NationalCode == nationalCode))
            {
                throw new DomainException("شخص با این کد ملی قبلا ثبت شده است");
            }
        }
        public async Task CheckPromoterIsExistForUpdate(Guid id, string nationalCode)
        {
            if (await _repository.AsQuery().AnyAsync(p => p.NationalCode == nationalCode && p.Id != id))
            {
                throw new DomainException("شخص با این کد ملی قبلا ثبت شده است");
            }
        }
        public void PromoterCustomerSubsetPaidFactor(Customer customer)
        {
            var customerSubset = _repository.AsQuery().SelectMany(p => p.CustomerSubsets)
                .FirstOrDefault(p => p.Customer.Id == customer.Id);
            if (customerSubset != null)
            {
                customerSubset.SetHavePaidFactor();
            }
        }
    }
}