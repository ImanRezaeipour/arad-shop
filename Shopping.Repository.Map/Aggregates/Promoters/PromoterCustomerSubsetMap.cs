using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Promoters.Entities;

namespace Shopping.Repository.Map.Aggregates.Promoters
{
    public class PromoterCustomerSubsetMap : EntityTypeConfiguration<PromoterCustomerSubset>
    {
        public PromoterCustomerSubsetMap()
        {
            HasKey(p => p.Id);
        }
    }
}