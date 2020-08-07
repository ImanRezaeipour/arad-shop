using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;

namespace Shopping.Repository.Map.Aggregates.Orders.Discounts
{
    public class OrderItemPercentDiscountMap : EntityTypeConfiguration<OrderItemPercentDiscount>
    {
        public OrderItemPercentDiscountMap()
        {
            ToTable("OrderItemPercentDiscount");
        }
    }
}