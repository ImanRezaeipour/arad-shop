using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Discounts.Entities;
using Shopping.QueryModel.QueryModels.Discounts;
using Shopping.QueryModel.QueryModels.Discounts.Abstract;

namespace Shopping.QueryService.Implements.Discounts
{
    public static class DiscountMapper
    {
        public static IPercentDiscountWithProductDto ToDiscountWithProductDto(this PercentDiscount src)
        {
            return AutoMapper.Mapper.Map<IPercentDiscountWithProductDto>(src);
        }

        public static IPercentDiscountDto ToDiscountDto(this PercentDiscount src)
        {
            return AutoMapper.Mapper.Map<IPercentDiscountDto>(src);
        }

        public static IDiscountBaseDto ToDto(this DiscountBase src)
        {
            return AutoMapper.Mapper.Map<IDiscountBaseDto>(src);
        }

        public static IProductDiscountDto ToDto(this ProductDiscount src)
        {
            return AutoMapper.Mapper.Map<IProductDiscountDto>(src);
        }
    }
}