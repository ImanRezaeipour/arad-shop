using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Messages;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Messages
{
    public class CustomerPrivateMessageController : ApiMobileControllerBase
    {
        private readonly IMessageQueryService _messageQueryService;
        public CustomerPrivateMessageController(IMessageQueryService messageQueryService)
        {
            _messageQueryService = messageQueryService;
        }
        [Authorize(Roles = "Customer")]
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_messageQueryService.GetCustomerPrivateMessageByUserId(UserId, pagedInput));
        }
    }
}