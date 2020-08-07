using AutoMapper;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Aggregates;
using Shopping.QueryModel.QueryModels.ShopMarketersHistories;

namespace Shopping.QueryService.Implements.ShopMarketersHistories
{
    public class ShopMarketersHistoryProfile : Profile
    {
        public ShopMarketersHistoryProfile()
        {
            CreateMap<ShopMarketersHistory, IShopMarketersHistoryDto>();
        }
    }
}