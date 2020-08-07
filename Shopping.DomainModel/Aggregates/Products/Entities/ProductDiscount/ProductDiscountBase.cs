using System;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount
{
    public abstract class ProductDiscountBase : Entity
    {
        protected ProductDiscountBase()
        { }
        protected ProductDiscountBase(Guid id, Guid discountId, string title, UserInfo userInfo,
            DateTime fromDate, DateTime toDate, DiscountType discountType)
        {
            DiscountType = discountType;
            Id = id;
            Title = title;
            UserInfo = userInfo;
            FromDate = fromDate;
            ToDate = toDate;
            DiscountId = discountId;
        }
        public Guid DiscountId { get; set; }
        public string Title { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public abstract bool HasDiscountValid();
        public override void Validate()
        {
        }
    }
}