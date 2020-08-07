using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.FakeIos.Orders.Commands;
using Shopping.Commands.Commands.FakeIos.Orders.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.FakeIos;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.FakeIos.Orders
{
    [Route("api/Ios/Order")]
    public class FakeOrderIosController : ApiMobileControllerBase
    {
        private readonly IFakeIosQueryService _fakeIosQueryService;
        public FakeOrderIosController(ICommandBus bus, IFakeIosQueryService fakeIosQueryService) : base(bus)
        {
            _fakeIosQueryService = fakeIosQueryService;
        }
        public IHttpActionResult Get(FakeOrderIosState? state)
        {
            var result = _fakeIosQueryService.GetOrders(state);
            return Ok(result);
        }
        [Route("api/Ios/Order/Count")]
        public IHttpActionResult GetCount(FakeOrderIosState? state)
        {
            var result = _fakeIosQueryService.GetOrdersCount(state);
            return Ok(result);
        }
        public async Task<IHttpActionResult> Post(CreateFakeOrderIosCommand command)
        {
            var commandResponse =
                await Bus.Send<CreateFakeOrderIosCommand, FakeOrderIosCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Order was registered successfully",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        public async Task<IHttpActionResult> Put(ChangeFakeOrderIosCommand command)
        {
            var commandResponse =
                await Bus.Send<ChangeFakeOrderIosCommand, FakeOrderIosCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Order was modified successfully",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}