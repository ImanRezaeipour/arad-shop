using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.FakeIos.Orders;

namespace Shopping.Repository.Map.Aggregates.FakeIos
{
    public class FakeOrderIosMap : EntityTypeConfiguration<FakeOrderIos>
    {
        public FakeOrderIosMap()
        {
            HasKey(p => p.Id);
            HasMany(p => p.Items);
        }
    }
}