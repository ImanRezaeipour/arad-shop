using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Sliders.Commands;
using Shopping.Commands.Commands.Sliders.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.Sliders;

namespace Shopping.ApiService.Controllers.Controllers.Sliders
{
    public class AddSliderToDiscountController : ApiControllerBase
    {
        private readonly ISliderQueryService _sliderQueryService;
        public AddSliderToDiscountController(ICommandBus bus, ISliderQueryService sliderQueryService):base(bus)
        {
            _sliderQueryService = sliderQueryService;
        }
        public async Task<IHttpActionResult> Put(AddSliderToDiscountCommand command)
        {
            var response = await Bus.Send<AddSliderToDiscountCommand, AddSliderToDiscountCommandResponse>(command);
            return Ok(response);
        }

        public IHttpActionResult Get()
        {
            return Ok(_sliderQueryService.GetDiscountsSlider());
        }
    }
}