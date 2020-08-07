using System;
using System.Collections.Generic;

namespace Shopping.QueryModel.QueryModels.BaseEntities
{
    public interface IProvinceDto
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        IList<ICityDto> Cities { get; set; }
    }
}