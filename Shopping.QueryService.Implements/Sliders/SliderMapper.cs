using AutoMapper;
using Shopping.DomainModel.Aggregates.Sliders.Aggregates;
using Shopping.QueryModel.QueryModels.Sliders;

namespace Shopping.QueryService.Implements.Sliders
{
    public static class SliderMapper
    {
        public static ISliderDto ToDto(this Slider src)
        {
            return Mapper.Map<ISliderDto>(src);
        }
        public static ISliderDiscountDto ToDiscountDto(this Slider src)
        {
            return Mapper.Map<ISliderDiscountDto>(src);
        }
    }
}