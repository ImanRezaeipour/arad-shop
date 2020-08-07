using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Sliders;

namespace Shopping.QueryModel.Implements.Slider
{
    public class SliderDiscount: ISliderDiscountDto
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; }
        public SliderType SliderType { get; set; }
        public string DiscountTitle { get; set; }
        public Guid AdditionalData { get; set; }
        public int Order { get; set; }
    }
}