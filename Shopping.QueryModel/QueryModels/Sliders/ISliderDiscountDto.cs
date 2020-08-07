using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Sliders
{
    public interface ISliderDiscountDto
    {
        Guid Id { get; set; }
        string ImageName { get; set; }
        bool IsActive { get; set; }
        SliderType SliderType { get; set; }
        string DiscountTitle { get; set; }
        Guid AdditionalData { get; set; }
        int Order { get; set; }
    }
}