using System;
using System.Web.Http;
using Shopping.Infrastructure.Core.PersianHelpers;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Marketers
{
    [RoutePrefix("api/MarketerShops")]
    [Authorize(Roles = "Support,Admin")]
    public class MarketerShopsController : ApiControllerBase
    {
        private readonly IShopQueryService _shopQueryService;
        public MarketerShopsController(IShopQueryService shopQueryService)
        {
            _shopQueryService = shopQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get(long id, string fromDate = "", string toDate = "")
        {
            var from = string.IsNullOrEmpty(fromDate) ? DateTime.Today : fromDate.ConvertGregorianDateTime();
            var to = string.IsNullOrEmpty(toDate) ? DateTime.Today : toDate.ConvertGregorianDateTime();
            var result = _shopQueryService.GetShopByMarketerId(id, from, to);
            return Ok(result);
        }
        //public async Task<IHttpActionResult> Put(UpdateMarketerCommand command)
        //{
        //    var response = await Bus.Send<UpdateMarketerCommand, UpdateMarketerCommandResponse>(command);
        //    return Ok(response);
        //}


    }
}