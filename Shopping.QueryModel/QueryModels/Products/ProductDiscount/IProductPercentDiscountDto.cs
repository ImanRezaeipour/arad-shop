using System;

namespace Shopping.QueryModel.QueryModels.Products.ProductDiscount
{
    public interface IProductPercentDiscountDto: IProductDiscountBaseDto
    {
        float Percent { get; set; }
        TimeSpan FromTime { get; set; }
        TimeSpan ToTime { get; set; }
    }
}