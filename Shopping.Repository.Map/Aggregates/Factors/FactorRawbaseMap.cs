using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Factors.Entities;

namespace Shopping.Repository.Map.Aggregates.Factors
{
    public class FactorRawbaseMap : EntityTypeConfiguration<FactorRaw>
    {
        public FactorRawbaseMap()
        {
            HasKey(i => i.Id);
        }
    }
}