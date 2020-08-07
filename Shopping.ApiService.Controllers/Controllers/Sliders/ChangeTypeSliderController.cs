using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Sliders.Commands;
using Shopping.Commands.Commands.Sliders.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Sliders
{
    public class ChangeTypeSliderController:ApiControllerBase
    {
        public ChangeTypeSliderController(ICommandBus bus):base(bus)
        {
            
        }

        public async Task<IHttpActionResult> Put(ChangeTypeSliderToDiscountCommand command)
        {
            var response = await Bus.Send<ChangeTypeSliderToDiscountCommand, ChangeTypeSliderToDiscountCommandResponse>(command);
            return Ok(response);
        }


    }
}