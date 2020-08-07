using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Orders.AreaOrderMap
{
    public class AreaOrderMap : EntityTypeConfiguration<AreaOrder>
    {
        public AreaOrderMap()
        {
            ToTable("AreaOrder");
        }
    }
}