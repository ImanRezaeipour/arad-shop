using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.Infrastructure.Core;
using Shopping.QueryModel.QueryModels.Persons.AppInfo;
using Shopping.QueryService.Interfaces.Persons;
using Shopping.Repository.Read.Interface;
using Shopping.Shared.Enums;

namespace Shopping.QueryService.Implements.Persons
{
    public class PersonQueryService : IPersonQueryService
    {
        private readonly IReadOnlyRepository<Person, Guid> _repository;
        public PersonQueryService(IReadOnlyRepository<Person, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<IAppInfoDto> GetAppInfo(Guid userId, string authDeviceId, AppType appType)
        {
            switch (appType)
            {
                case AppType.Shop:
                    var shop =await _repository.AsQuery().OfType<Shop>().SingleOrDefaultAsync(p => p.UserId == userId);
                    if (shop == null)
                    {
                        throw new DomainException("فروشگاه یافت نشد");
                    }
                    var appInfo = shop.AppInfos.SingleOrDefault(p => p.AuthDeviceId == authDeviceId);
                    if (appInfo == null)
                    {
                        throw new DomainException("تنظیمات یافت نشد");
                    }
                    return appInfo.ToDto();
                case AppType.Customer:
                    var customer =await _repository.AsQuery().OfType<Customer>().SingleOrDefaultAsync(p => p.UserId == userId);
                    if (customer == null)
                    {
                        throw new DomainException("مشتری یافت نشد");
                    }
                    var appInfoCustomer = customer.AppInfos.SingleOrDefault(p => p.AuthDeviceId == authDeviceId);
                    if (appInfoCustomer == null)
                    {
                        throw new DomainException("تنظیمات یافت نشد");
                    }
                    return appInfoCustomer.ToDto();
                default:
                    throw new DomainException("پارامترهای ارسالی نامعتبر می باشد");
            }
        }
    }
}