using System;
using System.Web.Http;
using Shopping.Infrastructure.Core.PersianHelpers;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.ApiService.Controllers.Controllers.Factors
{
    [RoutePrefix("api/FactorFinancialReport")]
    public class FactorFinancialReportController : ApiControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public FactorFinancialReportController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get(string fromDate = "", string toDate = "")
        {
            var from = string.IsNullOrEmpty(fromDate) ? DateTime.Today : fromDate.ConvertGregorianDateTime();
            var to = string.IsNullOrEmpty(toDate) ? DateTime.Today : toDate.ConvertGregorianDateTime();
            return Ok(_factorQueryService.GetFactorReportFinancial(from, to));
        }

        [Route("Total")]
        public IHttpActionResult GetTotal(string fromDate = "", string toDate = "")
        {
            var from = string.IsNullOrEmpty(fromDate) ? DateTime.Today : fromDate.ConvertGregorianDateTime();
            var to = string.IsNullOrEmpty(toDate) ? DateTime.Today : toDate.ConvertGregorianDateTime();
            return Ok(_factorQueryService.GetTotalFactorReportFinancial(from, to));
        }
    }
}