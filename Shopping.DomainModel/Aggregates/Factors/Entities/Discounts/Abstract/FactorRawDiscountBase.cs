using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.Discounts.Abstract
{
    public abstract class FactorRawDiscountBase: Entity
    {
        protected FactorRawDiscountBase()
        {
            
        }
        protected FactorRawDiscountBase(Guid id, Guid discountId, string discountTitle, DateTime fromDate, DateTime toDate)
        {
            Id = id;
            DiscountId = discountId;
            DiscountTitle = discountTitle;
            FromDate = fromDate;
            ToDate = toDate;
        }
        public Guid DiscountId { get; set; }
        public string DiscountTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public abstract bool HasDiscountValid();
    }
}