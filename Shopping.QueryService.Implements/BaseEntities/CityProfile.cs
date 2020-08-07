using AutoMapper;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.QueryModel.QueryModels.BaseEntities;

namespace Shopping.QueryService.Implements.BaseEntities
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Province, IProvinceDto>();
            CreateMap<Province, IProvinceWithoutCity>();
            CreateMap<City, ICityDto>();
            CreateMap<City, ICityWithoutZoneDto>();
            CreateMap<City, IZoneDto>();
        }
    }
}