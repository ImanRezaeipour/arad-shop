using AutoMapper;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;
using Shopping.QueryModel.Implements.Brands;
using Shopping.QueryModel.QueryModels.Brands;

namespace Shopping.QueryService.Implements.Brands
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, IBrandDto>();
            CreateMap<Brand, BrandDto>();
        }
    }
}