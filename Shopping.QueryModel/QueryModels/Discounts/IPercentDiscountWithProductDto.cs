using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.Discounts.Abstract;

namespace Shopping.QueryModel.QueryModels.Discounts
{
    public interface IPercentDiscountWithProductDto : IDiscountBaseDto
    {
        float Percent { get; set; }
        IList<IProductDiscountDto> ProductDiscounts { get; set; }
    }
}