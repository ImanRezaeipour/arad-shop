using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.Sliders;

namespace Shopping.QueryService.Interfaces.Sliders
{
    public interface ISliderQueryService
    {
        IEnumerable<ISliderDto> GetAll();
        IEnumerable<ISliderDiscountDto> GetActiveSliders();
        IEnumerable<ISliderDiscountDto> GetDiscountsSlider();
    }
}