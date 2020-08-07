using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.ShopCustomerSubsetSettlements.Aggregates;
using Shopping.QueryModel.Implements;
using Shopping.QueryModel.Implements.ShopCustomerSubsetSettlements;
using Shopping.QueryService.Interfaces.ShopCustomerSubsetSettlements;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.ShopCustomerSubsetSettlements
{
    public class ShopCustomerSubsetSettlementQueryService : IShopCustomerSubsetSettlementQueryService
    {
        private readonly IReadOnlyRepository<ShopCustomerSubsetSettlement, Guid> _repository;
        public ShopCustomerSubsetSettlementQueryService(IReadOnlyRepository<ShopCustomerSubsetSettlement, Guid> repository)
        {
            _repository = repository;
        }

        public IQueryable<ShopCustomerSubsetSettlementDto> GetByShopId(Guid shopId)
        {
            var result = _repository.AsQuery().Where(p => p.Shop.Id == shopId)
                .Select(item => new ShopCustomerSubsetSettlementDto
                {
                    CreationTime = item.CreationTime,
                    UserInfo = new UserInfoDto
                    {
                        LastName = item.UserInfo.LastName,
                        UserId = item.UserInfo.UserId,
                        FirstName = item.UserInfo.FirstName
                    },
                    Amount = item.Amount,
                    Type = item.Type,
                    IsRegisteredInAccounting = item.IsRegisteredInAccounting,
                    AccountingId = item.AccountingId
                });
            return result;
        }
    }
}