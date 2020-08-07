using AutoMapper;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Discounts.Entities;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Discounts;
using Shopping.QueryModel.QueryModels.Discounts.Abstract;

namespace Shopping.QueryService.Implements.Discounts
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<DiscountBase, IDiscountBaseDto>()
                .Include<PercentDiscount, IPercentDiscountDto>();

            CreateMap<PercentDiscount, IPercentDiscountDto>()
                .ForMember(dest => dest.DiscountType, opt => opt.UseValue(DiscountType.PercentDiscount));

            CreateMap<PercentDiscount, IPercentDiscountWithProductDto>()
                .ForMember(dest => dest.DiscountType, opt => opt.UseValue(DiscountType.PercentDiscount));

            CreateMap<ProductDiscount, IProductDiscountDto>();
        }
    }
}