using AutoMapper;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates.Abstrct;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.ValueObjects;
using Shopping.QueryModel.Implements.ProductsSuggestions;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.Abstract;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.CustomerProductsSuggestion;
using Shopping.QueryModel.QueryModels.ProductsSuggestions.ShopProductsSuggestion;

namespace Shopping.QueryService.Implements.ProductsSuggestions
{
    public class ProductSuggestionProfile : Profile
    {
        public ProductSuggestionProfile()
        {
            CreateMap<ProductSuggestion, IProductSuggestionDto>()
                .Include<CustomerProductSuggestion, ICustomerProductSuggestionDto>()
                .Include<ShopProductSuggestion, IShopProductSuggestionDto>();

            CreateMap<CustomerProductSuggestion, ICustomerProductSuggestionDto>()
                .ForMember(dest => dest.FullName, opt => opt.Ignore());

            CreateMap<ShopProductSuggestion, IShopProductSuggestionDto>()
                .ForMember(dest => dest.FullName, opt => opt.Ignore())
                .ForMember(dest => dest.ShopName, opt => opt.Ignore());

            CreateMap<ProductSuggestion, ProductSuggestionDto>()
                .ForMember(dest => dest.ProductSuggestionGroup, opt => opt.MapFrom(src => new ProductSuggestionGroupDto
                {
                    CategoryName = src.ProductSuggestionGroup.CategoryName,
                    BrandId = src.ProductSuggestionGroup.BrandId,
                    BrandName = src.ProductSuggestionGroup.BrandName,
                    CategoryId = src.ProductSuggestionGroup.CategoryId,
                    CategoryRootId = src.ProductSuggestionGroup.CategoryRootId,
                    CategoryRootName = src.ProductSuggestionGroup.CategoryRootName,
                }))
                .Include<ShopProductSuggestion, ShopProductSuggestionDto>()
                .Include<CustomerProductSuggestion, CustomerProductSuggestionDto>();

            CreateMap<ShopProductSuggestion, ShopProductSuggestionDto>();
            CreateMap<CustomerProductSuggestion, CustomerProductSuggestionDto>();
            CreateMap<ProductSuggestionGroup, ProductSuggestionGroupDto>();
        }

    }
}