using System;

namespace Shopping.QueryModel.QueryModels.Orders.Discounts
{
    public interface IOrderItemPercentDiscountDto: IOrderItemDiscountBaseDto
    {
         float Percent { get; set; }
         TimeSpan FromTime { get; set; }
         TimeSpan ToTime { get; set; }
    }
}