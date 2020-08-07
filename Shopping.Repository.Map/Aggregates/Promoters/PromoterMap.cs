using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Promoters.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Promoters
{
    public class PromoterMap : EntityTypeConfiguration<Promoter>
    {
        public PromoterMap()
        {
            HasKey(p => p.Id);
            HasMany(item => item.CustomerSubsets);
        }
    }
}