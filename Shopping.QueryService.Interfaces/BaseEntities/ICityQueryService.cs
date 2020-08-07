using System;
using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.BaseEntities;

namespace Shopping.QueryService.Interfaces.BaseEntities
{
    public interface ICityQueryService
    {
        IList<IProvinceDto> GetAll();
        IProvinceDto GetById(Guid id);
        IList<IProvinceWithoutCity> GetAllProvinecWithoutCities();
        IList<ICityWithoutZoneDto> GetAllCityWithoutZoneDtosByProvinecId(Guid provinecId);
        IEnumerable<IZoneDto> GetZoneByCityId(Guid cityId);
    }
}