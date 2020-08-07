using System;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;

namespace Shopping.DomainModel.Aggregates.Promoters.Interfaces
{
    public interface IPromoterDomainService
    {
        Task CheckPromoterIsExist(string nationalCode);
        Task CheckPromoterIsExistForUpdate(Guid id, string nationalCode);
        void PromoterCustomerSubsetPaidFactor(Customer customer);
    }
}