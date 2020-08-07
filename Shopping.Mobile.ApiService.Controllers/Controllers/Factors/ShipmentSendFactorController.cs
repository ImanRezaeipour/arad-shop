using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Factors.Commands;
using Shopping.Commands.Commands.Factors.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    public class ShipmentSendFactorController : ApiMobileControllerBase
    {
        public ShipmentSendFactorController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Shop")]
        public async Task<IHttpActionResult> Put(ShipmentSendFactorCommand command)
        {
            var commandResponse = await Bus.Send<ShipmentSendFactorCommand, ShipmentSendFactorCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت ارسال فاکتو با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}