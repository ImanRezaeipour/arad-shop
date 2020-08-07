using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Messages;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Messages
{
    public class ShopPrivateMessageController : ApiMobileControllerBase
    {
        private readonly IMessageQueryService _messageQueryService;
        public ShopPrivateMessageController(IMessageQueryService messageQueryService)
        {
            _messageQueryService = messageQueryService;
        }
        [Authorize(Roles = "Shop")]
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_messageQueryService.GetShopPrivateMessageByUserId(UserId, pagedInput));
        }
    }
}