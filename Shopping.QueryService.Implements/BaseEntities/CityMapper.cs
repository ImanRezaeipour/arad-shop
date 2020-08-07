using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.QueryModel.QueryModels.BaseEntities;

namespace Shopping.QueryService.Implements.BaseEntities
{
    public static class CityMapper
    {
        public static IProvinceDto ToDto(this Province src)
        {
            return AutoMapper.Mapper.Map<IProvinceDto>(src);
        }

        public static IProvinceWithoutCity ToProvinecWithoutCityDto(this Province src)
        {
            return AutoMapper.Mapper.Map<IProvinceWithoutCity>(src);
        }

        public static ICityWithoutZoneDto ToCityWithoutZoneDto(this City src)
        {
            return AutoMapper.Mapper.Map<ICityWithoutZoneDto>(src); 
        }

        public static IZoneDto ToDto(this Zone src)
        {
            return AutoMapper.Mapper.Map<IZoneDto>(src);
        }
    }
}