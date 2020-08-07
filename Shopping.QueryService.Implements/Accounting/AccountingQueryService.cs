using System;
using System.Configuration;
using System.Linq;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryService.Interfaces.Accounting;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Accounting
{
    public class AccountingQueryService : IAccountingQueryService
    {
        private readonly IReadOnlyRepository<Shop, Guid> _shopRepository;
        private const string ShopStatementServiceName = "ShopStatement";
        private const string SettlementDetailServiceName = "SettlementDetail";
        public AccountingQueryService(IReadOnlyRepository<Shop, Guid> shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public object GetShopRemain(Guid userId, string fromDate, string toDate)
        {
            var shop = _shopRepository.AsQuery().SingleOrDefault(item => item.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            if (shop.Accounting == null)
            {
                throw new DomainException("اطلاعات حسابرسی یافت نشد لطفا با پشتیبانی تماس حاصل فرمایید");
            }
            using (var helper = HttpHelper.Create(ConfigurationManager.AppSettings["AccountingBaseUrl"]))
            {
                var response = helper.Post<object>(ShopStatementServiceName, new ShopStatementServiceRequest
                {
                    ToDate = toDate,
                    FromDate = fromDate,
                    RequestId = Guid.NewGuid(),
                    DetailCode = shop.Accounting.DetailCode
                });
                return response;
            }
        }

        public object GetShopSettlement(Guid userId, string fromDate, string toDate)
        {
            var shop = _shopRepository.AsQuery().SingleOrDefault(item => item.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            if (shop.Accounting == null)
            {
                throw new DomainException("اطلاعات حسابرسی یافت نشد لطفا با پشتیبانی تماس حاصل فرمایید");
            }
            using (var helper = HttpHelper.Create(ConfigurationManager.AppSettings["AccountingBaseUrl"]))
            {
                var response = helper.Post<object>(SettlementDetailServiceName, new ShopStatementServiceRequest
                {
                    ToDate = toDate,
                    FromDate = fromDate,
                    RequestId = Guid.NewGuid(),
                    DetailCode = shop.Accounting.DetailCode
                });
                return response;
            }
        }
    }
}