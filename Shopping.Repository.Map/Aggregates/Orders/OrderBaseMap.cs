using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;

namespace Shopping.Repository.Map.Aggregates.Orders
{
    public class OrderBaseMap : EntityTypeConfiguration<OrderBase>
    {
        public OrderBaseMap()
        {
            HasKey(p => p.Id);
            HasMany(p => p.OrderItems);
            Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}