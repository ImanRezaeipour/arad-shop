using System;
using System.Web.Http;
using Shopping.Infrastructure.Core.PersianHelpers;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops.CustomerSubsets
{
    public class ShopsCustomerSubsetReportController : ApiControllerBase
    {
        private readonly IShopQueryService _shopQueryService;
        public ShopsCustomerSubsetReportController(IShopQueryService shopQueryService)
        {
            _shopQueryService = shopQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get(string fromDate = "", string toDate = "")
        {
            var from = string.IsNullOrEmpty(fromDate) ? DateTime.Today : fromDate.ConvertGregorianDateTime();
            var to = string.IsNullOrEmpty(toDate) ? DateTime.Today : toDate.ConvertGregorianDateTime();
            var result =  _shopQueryService.GetShopsCustomerSubsetReport(from, to);
            return Ok(result);
        }
    }
}