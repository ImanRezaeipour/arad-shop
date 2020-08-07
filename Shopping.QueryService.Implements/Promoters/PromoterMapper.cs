using AutoMapper;
using Shopping.DomainModel.Aggregates.Promoters.Aggregates;
using Shopping.QueryModel.QueryModels.Promoters;

namespace Shopping.QueryService.Implements.Promoters
{
    public static class PromoterMapper
    {
        public static IPromoterDto ToDto(this Promoter src)
        {
            return Mapper.Map<IPromoterDto>(src);
        }
    }
}