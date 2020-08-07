using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.FakeIos.Orders;

namespace Shopping.Repository.Map.Aggregates.FakeIos
{
    public class FakeOrderIosItemMap : EntityTypeConfiguration<FakeOrderIosItem>
    {
        public FakeOrderIosItemMap()
        {
            HasKey(p => p.Id);
        }
    }
}