using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Orders.Commands.PrivateOrders;
using Shopping.Commands.Commands.Orders.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Orders
{
    [RoutePrefix("api/PrivateOrder")]
    public class PrivateOrderController : ApiMobileControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        public PrivateOrderController(ICommandBus bus, IOrderQueryService orderQueryService) : base(bus)
        {
            _orderQueryService = orderQueryService;
        }

        public IHttpActionResult Get(long id)
        {
            var result = new MobileResultDto
            {
                Result = _orderQueryService.Get(id)
            };
            return Ok(result);
        }
        [Route("State/{id}")]
        //   [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> GetState(long id)
        {
            return Ok(new MobileResultDto
            {
                Result = await _orderQueryService.CheckOrderState(id)
            });
        }
        //   [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Post(CreatePrivateOrderCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<CreatePrivateOrderCommand, CreatePrivateOrderCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Order has been successfully issued",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Route("Opened")]
        //[Authorize(Roles = "Shop")]
        public async Task<IHttpActionResult> PutOpened(OpenPrivateOrderCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<OpenPrivateOrderCommand, OpenPrivateOrderCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Order has been successfully opened",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Route("ConvertPrivateToAreaOrder")]
        //[Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> PutConvertPrivateToAreaOrder(ConvertPrivateToAreaOrderCommand command)
        {

            var commandResponse = await Bus.Send<ConvertPrivateToAreaOrderCommand, ConvertPrivateToAreaOrderCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Order for other shops has been successfully issued",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Route("CancelPrivateOrder")]
        //todo [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> PutCancelPrivateOrder(CancelPrivateOrderConmmand command)
        {

            var commandResponse = await Bus.Send<CancelPrivateOrderConmmand, CancelPrivateOrderConmmandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Order has been successfully canceled",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}