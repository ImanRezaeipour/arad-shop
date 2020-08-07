using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Payments.Commands;
using Shopping.Commands.Commands.Payments.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Payments
{
    public class PaymentController : ApiMobileControllerBase
    {
        public PaymentController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Post(PaymentFactorCommand command)
        {
            var commandResponse = await Bus.Send<PaymentFactorCommand, PaymentFactorCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت پرداختی فاکتور با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}
