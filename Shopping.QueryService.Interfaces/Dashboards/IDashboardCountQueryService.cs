using System;
using Shopping.QueryModel.QueryModels.Dashboards;

namespace Shopping.QueryService.Interfaces.Dashboards
{
    public interface IDashboardCountQueryService
    {
        IDashboardCountDto GetAppInfoCountByCityId(Guid cityId);
    }
}