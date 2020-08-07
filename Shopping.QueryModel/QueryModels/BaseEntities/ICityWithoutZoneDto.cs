using System;

namespace Shopping.QueryModel.QueryModels.BaseEntities
{
    public interface ICityWithoutZoneDto
    {
        Guid Id { get; set; }
        string CityName { get; set; }
        string Code { get; set; }
    }
}