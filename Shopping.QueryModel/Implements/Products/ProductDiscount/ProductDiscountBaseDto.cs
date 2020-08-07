using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.Implements.Products.ProductDiscount
{
    public class ProductDiscountBaseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}