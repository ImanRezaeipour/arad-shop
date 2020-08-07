using AutoMapper;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Aggregates;
using Shopping.QueryModel.QueryModels.ShopMarketersHistories;

namespace Shopping.QueryService.Implements.ShopMarketersHistories
{
    public static class ShopMarketersHistoryMapper
    {
        public static IShopMarketersHistoryDto ToDto(this ShopMarketersHistory src)
        {
            return Mapper.Map<IShopMarketersHistoryDto>(src);
        }
    }
}