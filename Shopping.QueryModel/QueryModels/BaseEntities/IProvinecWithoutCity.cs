using System;

namespace Shopping.QueryModel.QueryModels.BaseEntities
{
    public interface IProvinceWithoutCity
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Code { get; set; }
    }
}