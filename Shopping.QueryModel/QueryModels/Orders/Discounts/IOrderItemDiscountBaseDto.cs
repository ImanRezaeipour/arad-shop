using System;

namespace Shopping.QueryModel.QueryModels.Orders.Discounts
{
    public interface IOrderItemDiscountBaseDto
    {
        Guid DiscountId { get; set; }
        string DiscountTitle { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}