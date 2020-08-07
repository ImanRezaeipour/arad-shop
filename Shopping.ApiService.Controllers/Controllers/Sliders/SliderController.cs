using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Sliders.Commands;
using Shopping.Commands.Commands.Sliders.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.Sliders;

namespace Shopping.ApiService.Controllers.Controllers.Sliders
{
    [RoutePrefix("api/Slider")]
    [Authorize(Roles = "Support,Admin")]
    public class SliderController : ApiControllerBase
    {
        private readonly ISliderQueryService _sliderQueryService;
        public SliderController(ICommandBus bus, ISliderQueryService sliderQueryService) : base(bus)
        {
            _sliderQueryService = sliderQueryService;
        }
        /// <summary>
        /// دریافت لیست کامل اسلایدر
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            return Ok(_sliderQueryService.GetAll());
        }
        /// <summary>
        /// حذف اسلایدر
        /// </summary>
        /// <param name="sliderId"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(Guid sliderId)
        {
            var command = new DeleteSlideCommand
            {
                Id = sliderId
            };
            var response = await Bus.Send<DeleteSlideCommand, DeleteSlideCommandResponse>(command);
            return Ok(response);
        }
        /// <summary>
        /// ایجاد اسلایدر
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(CreateSliderCommand command)
        {
            var response = await Bus.Send<CreateSliderCommand, CreateSliderCommandResponse>(command);
            return Ok(response);
        }
        [Route("Active")]
        public async Task<IHttpActionResult> Patch(ActiveSliderCommand command)
        {
            var response = await Bus.Send<ActiveSliderCommand, ActivationSliderCommandResponse>(command);
            return Ok(response);
        }
        [Route("DeActive")]
        public async Task<IHttpActionResult> Patch(DeActiveSliderCommand command)
        {
            var response = await Bus.Send<DeActiveSliderCommand, ActivationSliderCommandResponse>(command);
            return Ok(response);
        }
        [Route("Sort")]
        public async Task<IHttpActionResult> PutSort(SortOrderSliderCommand command)
        {
            var response = await Bus.Send<SortOrderSliderCommand, SortOrderSliderCommandResponse>(command);
            return Ok(response);
        }
    }
}