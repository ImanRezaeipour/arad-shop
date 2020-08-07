using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Dashboards;

namespace Shopping.ApiService.Controllers.Controllers.Dashboards
{
    public class DashboardCountController:ApiControllerBase
    {
        private readonly IDashboardCountQueryService _dashboardCountQueryService;
        public DashboardCountController(IDashboardCountQueryService dashboardCountQueryService)
        {
            _dashboardCountQueryService = dashboardCountQueryService;
        }
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid cityId)
        {
            return Ok(_dashboardCountQueryService.GetAppInfoCountByCityId(cityId));
        }
    }
}