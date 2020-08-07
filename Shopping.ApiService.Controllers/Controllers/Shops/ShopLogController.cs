

using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class ShopLogController :ApiControllerBase
    {
        private readonly IShopQueryService _personQueryService;

        public ShopLogController(IShopQueryService personQueryService)
        {
            _personQueryService = personQueryService;
        }

        
        public IHttpActionResult Get(Guid shopId)
        {
            return Ok(_personQueryService.GetLogChangesById(shopId));
        }
    }
}