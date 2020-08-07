using System.Data.Entity.ModelConfiguration;

namespace Shopping.Repository.Map.Aggregates.Persons.Shops
{
    public class ShopMap : EntityTypeConfiguration<DomainModel.Aggregates.Persons.Aggregates.Shop>
    {
        public ShopMap()
        {
            ToTable("Shop");
            HasMany(p => p.ShopStatusLogs);
            HasMany(p => p.CustomerSubsets);
        }
    }
}