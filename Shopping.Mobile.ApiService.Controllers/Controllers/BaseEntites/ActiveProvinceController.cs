using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.BaseEntities;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.BaseEntites
{
    public class ActiveProvinceController: ApiMobileControllerBase
    {
        private readonly ICityQueryService _cityQueryService;

        public ActiveProvinceController(ICityQueryService cityQueryService)
        {
            _cityQueryService = cityQueryService;
        }
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _cityQueryService.GetAllProvinecWithoutCities()
            };
            return Ok(dto);
        }
    }
}