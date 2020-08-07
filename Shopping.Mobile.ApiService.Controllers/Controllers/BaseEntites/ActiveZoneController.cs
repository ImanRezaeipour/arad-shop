using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.BaseEntities;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.BaseEntites
{
    public class ActiveZoneController: ApiMobileControllerBase
    {
        private readonly ICityQueryService _cityQueryService;

        public ActiveZoneController(ICityQueryService cityQueryService)
        {
            _cityQueryService = cityQueryService;
        }
        public IHttpActionResult Get(Guid cityId)
        {
            var dto = new MobileResultDto
            {
                Result = _cityQueryService.GetZoneByCityId(cityId)
            };
            return Ok(dto);
        }
    }
}