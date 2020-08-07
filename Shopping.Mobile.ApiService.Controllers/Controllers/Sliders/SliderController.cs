using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Sliders;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Sliders
{
    public class SliderController : ApiMobileControllerBase
    {
        private readonly ISliderQueryService _sliderQueryService;
        public SliderController(ISliderQueryService sliderQueryService)
        {
            _sliderQueryService = sliderQueryService;
        }
        /// <summary>
        /// دریافت لیست کامل اسلایدر
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _sliderQueryService.GetActiveSliders()
            };
            return Ok(dto);
        }
    }
}