using System;
using Shopping.QueryModel.QueryModels.Discounts.Abstract;

namespace Shopping.QueryModel.QueryModels.Discounts
{
    public interface IPercentDiscountDto: IDiscountBaseDto
    {
        float Percent { get; set; }
        int MaxOrderCount { get; set; }
        int MaxProductCount { get; set; }
        TimeSpan FromTime { get; set; }
        TimeSpan ToTime { get; set; }
        int RemainOrderCount { get; set; }
    }
}