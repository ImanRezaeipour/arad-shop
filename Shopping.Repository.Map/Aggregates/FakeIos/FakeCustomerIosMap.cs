using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.FakeIos.Customers;

namespace Shopping.Repository.Map.Aggregates.FakeIos
{
    public class FakeCustomerIosMap : EntityTypeConfiguration<FakeCustomerIos>
    {
        public FakeCustomerIosMap()
        {
            HasKey(p => p.Id);
        }
    }
}