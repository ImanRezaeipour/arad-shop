using System;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount
{
    public class ProductPercentDiscount : ProductDiscountBase
    {
        protected ProductPercentDiscount()
        { }

        public ProductPercentDiscount(Guid id, Guid discountId, string title, UserInfo userInfo, DateTime fromDate, DateTime toDate, DiscountType discountType, float percent, TimeSpan fromTime, TimeSpan toTime) : base(id, discountId, title, userInfo, fromDate, toDate, discountType)
        {
            Percent = percent;
            FromTime = fromTime;
            ToTime = toTime;
            DiscountType = discountType;
        }

        public float Percent { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public override bool HasDiscountValid()
        {
            var now = DateTime.Now;
            var today = DateTime.Today;
            return FromDate <= today &&
                   ToDate >= today &&
                   FromTime <= now.TimeOfDay &&
                   ToTime >= now.TimeOfDay;
        }
    }
}