using System;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;

namespace Shopping.DomainModel.Aggregates.Persons.Interfaces
{
    public interface IPersonDomainService
    {
        void AddressIsDefaultAddress(Customer customer, Guid cityId);
        void CheckCustomerIsExist(Guid userId);
        void CheckShopIsExist(Guid userId);
        void SetCustomerRecommender(long? code, Customer customer);
        void ShopCustomerSubsetPaidFactor(Shop shop, Customer customer);
    }
}