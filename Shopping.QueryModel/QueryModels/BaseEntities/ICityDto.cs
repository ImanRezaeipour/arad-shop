using System;
using System.Collections.Generic;

namespace Shopping.QueryModel.QueryModels.BaseEntities
{
    public interface ICityDto
    {
        Guid Id { get; set; }
        string CityName { get; set; }
        bool IsActive { get; set; }
        string Code { get; set; }
        IList<IZoneDto> Zones { get; set; }
    }
}