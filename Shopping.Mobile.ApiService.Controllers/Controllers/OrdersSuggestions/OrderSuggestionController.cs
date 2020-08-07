using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.OrdersSuggestions.Commands;
using Shopping.Commands.Commands.OrdersSuggestions.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.OrdersSuggestions;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.OrdersSuggestions
{
    public class OrderSuggestionController : ApiMobileControllerBase
    {
        private readonly IOrderSuggestionQueryService _orderSuggestionQueryService;
        public OrderSuggestionController(ICommandBus bus, IOrderSuggestionQueryService orderSuggestionQueryService) : base(bus)
        {
            _orderSuggestionQueryService = orderSuggestionQueryService;
        }
        [Authorize(Roles = "Shop")]
        public async Task<IHttpActionResult> Post(CreateOrderSuggestionCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<CreateOrderSuggestionCommand, CreateOrderSuggestionCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت پیشنهاد سفارش با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        //todo[Authorize]
        public IHttpActionResult Get(long orderId)
        {
            var dto = new MobileResultDto
            {
                Result = _orderSuggestionQueryService.GetByOrderId(orderId)
            };
            return Ok(dto);
        }
    }
}