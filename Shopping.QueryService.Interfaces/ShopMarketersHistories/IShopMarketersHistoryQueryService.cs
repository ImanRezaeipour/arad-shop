using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.QueryModel.QueryModels.ShopMarketersHistories;

namespace Shopping.QueryService.Interfaces.ShopMarketersHistories
{
    public interface IShopMarketersHistoryQueryService
    {
        Task<IList<IShopMarketersHistoryDto>> GetShopMarketers(Guid shopId);
    }
}