using AutoMapper;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Entities;
using Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount;
using Shopping.DomainModel.Aggregates.Products.ValueObjects;
using Shopping.QueryModel.Implements.Products;
using Shopping.QueryModel.QueryModels.Products;
using Shopping.QueryModel.QueryModels.Products.ProductDiscount;

namespace Shopping.QueryService.Implements.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, IProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(desc => desc.DiscountPrice, opt => opt.ResolveUsing(CalcDiscountPrice));


            CreateMap<Product, IProductWithDiscountDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(desc => desc.DiscountIsValid, opt => opt.ResolveUsing(CheckIsValidDiscount))
                .ForMember(desc => desc.DiscountPrice, opt => opt.ResolveUsing(CalcDiscountPrice));


            CreateMap<Product, IProductWithImageDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(desc => desc.DiscountIsValid, opt => opt.ResolveUsing(CheckIsValidDiscount))
                .ForMember(desc => desc.DiscountPrice, opt => opt.ResolveUsing(CalcDiscountPrice));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

            CreateMap<ProductDiscountBase, IProductDiscountBaseDto>()
                .Include<ProductPercentDiscount, IProductPercentDiscountDto>();

            CreateMap<ProductPercentDiscount, IProductPercentDiscountDto>();

            CreateMap<ProductImage, IProductImageDto>();
            CreateMap<FakeProductDiscount, IFakeProductDiscountDto>();
        }

        private static bool CheckIsValidDiscount(Product product)
        {
            if (product.ProductDiscount == null) return false;
            return product.ProductDiscount.HasDiscountValid();
        }

        private static decimal CalcDiscountPrice(Product product)
        {
            switch (product.ProductDiscount)
            {
                case null:
                    return 0;
                case ProductPercentDiscount percentDiscount:
                    return decimal.Round(product.Price * (decimal) (100 - percentDiscount.Percent) / 100);
            }
            return 0;
        }
      
    }
}