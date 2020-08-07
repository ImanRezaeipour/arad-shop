using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.ApiService.Controllers.Controllers.Factors
{
    public class FactorController:ApiControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public FactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_factorQueryService.GetAll());
        }
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(long id)
        {
            return Ok(_factorQueryService.GetById(id));
        }
    }
}