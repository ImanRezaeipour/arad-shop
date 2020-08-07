using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class CityShopsController : ApiControllerBase
    {
        private readonly IShopQueryService _personQueryService;
        public CityShopsController(IShopQueryService personQueryService)
        {
            _personQueryService = personQueryService;
        }
        [Authorize(Roles = "Support,Admin,Operator")]
        public IHttpActionResult Get(Guid cityId)
        {
            return Ok(_personQueryService.GetShopsByCityId(cityId));
        }
    }
}