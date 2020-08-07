using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Interfaces;
using Shopping.DomainModel.Aggregates.Persons.ValueObjects;
using Shopping.DomainModel.Aggregates.Promoters.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Persons.Services
{
    public class PersonDomainService : IPersonDomainService
    {
        private readonly IRepository<Person> _repository;
        private readonly IRepository<Promoter> _promoterRepository;
        private readonly IRepository<ApplicationSetting> _settingRepository;
        public PersonDomainService(IRepository<Person> repository,
            IRepository<Promoter> promoterRepository,
            IRepository<ApplicationSetting> settingRepository)
        {
            _repository = repository;
            _promoterRepository = promoterRepository;
            _settingRepository = settingRepository;
        }
        public void AddressIsDefaultAddress(Customer customer, Guid addressId)
        {
            if (customer.DefultCustomerAddress.CustomerAddressId == addressId)
            {
                throw new DomainException("ادرس انتخابی،ادرس پیش فرض است و قادر به حذف ان نمی باشید");
            }
        }
        public void CheckCustomerIsExist(Guid userId)
        {
            if (_repository.AsQuery().OfType<Customer>().Any(item => item.UserId == userId))
            {
                throw new DomainException("شما قبلا ثبت نام کرده اید");
            }
        }
        public void CheckShopIsExist(Guid userId)
        {
            if (_repository.AsQuery().OfType<Shop>().Any(item => item.UserId == userId))
            {
                throw new DomainException("شما قبلا ثبت نام کرده اید");
            }
        }
        public void SetCustomerRecommender(long? recommendCode, Customer customer)
        {
            if (recommendCode == null)
            {
                customer.Recommender = new CustomerRecommender(Guid.Empty, 0, null);
                return;
            }

            if (recommendCode.Value > 900000)
            {
                var promoter = _promoterRepository.AsQuery().SingleOrDefault(p => p.Code == recommendCode);
                if (promoter == null)
                {
                    throw new DomainException("کد معرف وارد شده نامعتبر می باشد");
                }
                promoter.AddCustomerSubset(customer);
                customer.Recommender = new CustomerRecommender(promoter.Id, promoter.Code, RecommenderType.Promoter);
            }
            else
            {
                var shop = _repository.AsQuery().OfType<Shop>().SingleOrDefault(p => p.RecommendCode == recommendCode);
                if (shop == null)
                {
                    throw new DomainException("کد معرف وارد شده نامعتبر می باشد");
                }
                if (!shop.IsActive)
                {
                    customer.Recommender = new CustomerRecommender(Guid.Empty, 0, null);
                    return;
                }
                shop.AddCustomerSubset(customer);
                customer.Recommender = new CustomerRecommender(shop.Id, shop.RecommendCode, RecommenderType.Promoter);
            }
        }
        public void ShopCustomerSubsetPaidFactor(Shop shop, Customer customer)
        {
            var today = DateTime.Today;
            var lastOfTimeToday = DateTime.Today + TimeSpan.Parse("23:59:59");
            var countPaidSubset = _settingRepository.AsQuery().FirstOrDefault()?.ShopCustomerSubsetHaveFactorPaidCount;
            if (shop.CustomerSubsets
                    .Count(p => p.CreationTime >= today && p.CreationTime <= lastOfTimeToday) <= countPaidSubset)
            {
                var customerSubset = shop.CustomerSubsets.SingleOrDefault(p => p.Customer.Id == customer.Id);
                if (customerSubset != null)
                {
                    customerSubset.SetHavePaidFactor();
                }
            }
        }
    }
}