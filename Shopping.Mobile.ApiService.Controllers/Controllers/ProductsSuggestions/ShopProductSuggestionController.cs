using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.ProductsSuggestions.Commands.ShopProductSuggestion;
using Shopping.Commands.Commands.ProductsSuggestions.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.ProductsSuggestion;

#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.ProductsSuggestions
{
    public class ShopProductSuggestionController : ApiMobileControllerBase
    {
        private readonly IShopProductsSuggestionQueryService _shopProductsSuggestionQuery;
        public ShopProductSuggestionController(ICommandBus bus, IShopProductsSuggestionQueryService shopProductsSuggestionQuery) : base(bus)
        {
            _shopProductsSuggestionQuery = shopProductsSuggestionQuery;
        }
        [Authorize(Roles = "Shop")]
        public async Task<IHttpActionResult> Post(CreateShopProductSuggestionCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<CreateShopProductSuggestionCommand, CreateShopProductSuggestionCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت کالای پیشنهادی با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        /// <summary>
        /// دریافت لیست محصولات پیشنهادی یک کاربر
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Shop")]
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_shopProductsSuggestionQuery.GetByUserId(UserId, pagedInput));
        }
    }
}