using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Orders.Entities;

namespace Shopping.Repository.Map.Aggregates.Orders.Items
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            HasKey(item => item.Id);
        }
    }
}