using System;

namespace Shopping.DomainModel.Aggregates.Orders.Entities.Discounts
{
    public class OrderItemPercentDiscount : OrderItemDiscountBase
    {
        protected OrderItemPercentDiscount()
        {
        }
        public OrderItemPercentDiscount(Guid id, Guid discountId, string discountTitle, DateTime fromDate, DateTime toDate, float percent, TimeSpan fromTime, TimeSpan toTime) : base(id,discountId, discountTitle, fromDate, toDate)
        {
            Percent = percent;
            FromTime = fromTime;
            ToTime = toTime;
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