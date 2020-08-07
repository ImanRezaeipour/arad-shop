using AutoMapper;
using Shopping.DomainModel.Aggregates.Sliders.Aggregates;
using Shopping.QueryModel.QueryModels.Sliders;

namespace Shopping.QueryService.Implements.Sliders
{
    public class SliderProfile:Profile
    {
        public SliderProfile()
        {
            CreateMap<Slider, ISliderDto>();
            CreateMap<Slider, ISliderDiscountDto>();
        }
    }
}