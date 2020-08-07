using System;
using System.Linq;
using Shopping.QueryModel.Implements.ShopCustomerSubsetSettlements;

namespace Shopping.QueryService.Interfaces.ShopCustomerSubsetSettlements
{
    public interface IShopCustomerSubsetSettlementQueryService
    {
        IQueryable<ShopCustomerSubsetSettlementDto> GetByShopId(Guid shopId);
    }
}