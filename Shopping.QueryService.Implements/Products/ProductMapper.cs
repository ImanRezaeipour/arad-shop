using AutoMapper;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.QueryModel.QueryModels.Products;

namespace Shopping.QueryService.Implements.Products
{
    public static class ProductMapper
    {
        public static IProductWithImageDto ToProductWithImageDto(this Product src)
        {
            return Mapper.Map<IProductWithImageDto>(src);
        }
        public static IProductDto ToProductDto(this Product src)
        {
            return Mapper.Map<IProductDto>(src);
        }
        public static IProductWithDiscountDto ToProductWithDiscountDto(this Product src)
        {
            return Mapper.Map<IProductWithDiscountDto>(src);
        }

     
        
    }
}