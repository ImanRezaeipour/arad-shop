using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;

namespace Shopping.Repository.Map.Aggregates.Orders.Discounts
{
    public class OrderItemDiscountBaseMap : EntityTypeConfiguration<OrderItemDiscountBase>
    {
        public OrderItemDiscountBaseMap()
        {
            HasKey(i => i.Id);
        }
    }
}