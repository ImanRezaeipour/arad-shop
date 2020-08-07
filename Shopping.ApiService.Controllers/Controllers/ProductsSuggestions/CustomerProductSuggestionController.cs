using System;
using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.ProductsSuggestion;

namespace Shopping.ApiService.Controllers.Controllers.ProductsSuggestions
{
    public class CustomerProductSuggestionController : ApiControllerBase
    {
        private readonly ICustomerProductsSuggestionQueryService _customerProductsSuggestionQueryService;
        public CustomerProductSuggestionController(ICustomerProductsSuggestionQueryService customerProductsSuggestionQueryService)
        {
            _customerProductsSuggestionQueryService = customerProductsSuggestionQueryService;
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_customerProductsSuggestionQueryService.GetAll());
        }
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_customerProductsSuggestionQueryService.GetById(id));
        }
    }
}