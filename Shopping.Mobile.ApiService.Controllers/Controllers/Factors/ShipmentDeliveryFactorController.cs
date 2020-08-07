using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Factors.Commands;
using Shopping.Commands.Commands.Factors.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    public class ShipmentDeliveryFactorController:ApiMobileControllerBase
    {
        public ShipmentDeliveryFactorController(ICommandBus bus):base(bus)
        {
        }
        [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Put(ShipmentDeliveryFactorCommand command)
        {
            var commandResponse = await Bus.Send<ShipmentDeliveryFactorCommand, ShipmentDeliveryFactorCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "تحویل فاکتو با موفقعیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}