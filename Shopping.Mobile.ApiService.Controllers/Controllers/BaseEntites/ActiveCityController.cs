using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.BaseEntities;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.BaseEntites
{
    public class ActiveCityController: ApiMobileControllerBase
    {
        private readonly ICityQueryService _cityQueryService;

        public ActiveCityController(ICityQueryService cityQueryService)
        {
            _cityQueryService = cityQueryService; 
        }
        public IHttpActionResult Get(Guid provinceId)
        {
            var dto = new MobileResultDto
            {
                Result = _cityQueryService.GetAllCityWithoutZoneDtosByProvinecId(provinceId)
            };
            return Ok(dto);
        }
    }
}