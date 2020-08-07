using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.BaseEntities;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.BaseEntites
{
    public class CitiesController : ApiMobileControllerBase
    {
        private readonly ICityQueryService _cityQueryService;
        public CitiesController(ICityQueryService cityQueryService)
        {
            _cityQueryService = cityQueryService;
        }
        /// <summary>
        /// دریافت تمام استان و شهر
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _cityQueryService.GetAll()
            };
            return Ok(dto);
        }
    }
}