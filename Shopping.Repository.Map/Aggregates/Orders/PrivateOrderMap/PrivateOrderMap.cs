using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Orders.PrivateOrderMap
{
    public class PrivateOrderMap : EntityTypeConfiguration<PrivateOrder>
    {
        public PrivateOrderMap()
        {
            ToTable("PrivateOrder");
        }
    }
}