using AutoMapper;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.QueryModel.QueryModels.Brands;

namespace Shopping.QueryService.Implements.Brands
{
    public static class BrandMapper
    {
        public static IBrandDto ToDto(this Brand src)
        {
            return Mapper.Map<IBrandDto>(src);
        }
    }
}