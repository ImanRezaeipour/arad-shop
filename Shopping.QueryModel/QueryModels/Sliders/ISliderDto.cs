using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Sliders
{
    public interface ISliderDto
    {
        Guid Id { get; set; }
        string ImageName { get; set; }
        bool IsActive { get; set; }
        SliderType SliderType { get; set; }
        int Order { get; set; }
    }
}