using System;
using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.ProductsSuggestion;

namespace Shopping.ApiService.Controllers.Controllers.ProductsSuggestions
{
    public class ShopProductSuggestionController:ApiControllerBase
    {
        private readonly IShopProductsSuggestionQueryService _shopProductsSuggestionQuery;
        public ShopProductSuggestionController( IShopProductsSuggestionQueryService shopProductsSuggestionQuery)
        {
            _shopProductsSuggestionQuery = shopProductsSuggestionQuery;
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_shopProductsSuggestionQuery.GetAll());
        }
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_shopProductsSuggestionQuery.GetById(id));
        }
    }
}