using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;

namespace Shopping.Repository.Map.Aggregates.BaseEntities
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            HasKey(p => p.Id);
            HasMany(p => p.Zones);
        }
    }
}