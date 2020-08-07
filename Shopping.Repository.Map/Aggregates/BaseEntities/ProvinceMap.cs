using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;

namespace Shopping.Repository.Map.Aggregates.BaseEntities
{
    public class ProvinceMap:EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            HasKey(p => p.Id);
            HasMany(p => p.Cities);
        }        
    }
}