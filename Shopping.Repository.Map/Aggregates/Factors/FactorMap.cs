using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Factors
{
    public class FactorMap : EntityTypeConfiguration<Factor>
    {
        public FactorMap()
        {
            HasKey(p => p.Id);
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasMany(p => p.FactorRaws);
        }
    }
}