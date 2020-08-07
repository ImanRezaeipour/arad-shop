using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.FakeIos.Customers.Commands;
using Shopping.Commands.Commands.FakeIos.Customers.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.FakeIos.Customers
{
    [Route("api/Ios/Customer")]
    public class FakeCustomerIosController : ApiMobileControllerBase
    {
        public FakeCustomerIosController(ICommandBus bus) : base(bus)
        {
        }
        public async Task<IHttpActionResult> Post(RegisterFakeCustomerIosCommand command)
        {
            var commandResponse = 
                await Bus.Send<RegisterFakeCustomerIosCommand, RegisterFakeCustomerIosCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Registration was successful",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Route("api/Ios/Customer/Login")]
        public async Task<IHttpActionResult> PostLogin(LoginFakeCustomerIosCommand command)
        {
            var commandResponse =
                await Bus.Send<LoginFakeCustomerIosCommand, LoginFakeCustomerIosCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "Login was successful",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}