using System;
using Shopping.QueryModel.QueryModels.Orders.Discounts;

namespace Shopping.QueryModel.QueryModels.Orders.Items
{
    public interface IOrderItemDto
    {
        Guid Id { get; set; }
        int Quantity { get; set; }
        string Description { get; set; }
        IOrderProductDto OrderProduct { get; set; }
        IOrderItemDiscountBaseDto Discount { get; set; }
        bool DiscountIsValid { get; set; }
    }
}