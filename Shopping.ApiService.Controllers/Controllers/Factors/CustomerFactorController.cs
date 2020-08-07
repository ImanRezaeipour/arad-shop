using System;
using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.ApiService.Controllers.Controllers.Factors
{
    public class CustomerFactorController : ApiControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public CustomerFactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid customerId)
        {
            return Ok(_factorQueryService.GetCustomerFactorsByCustomerId(customerId));
        }
    }
}