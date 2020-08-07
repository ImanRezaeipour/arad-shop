using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Persons.CustomerMaps
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customer");
            HasMany(p => p.CustomerAddresses);
        }
    }
}