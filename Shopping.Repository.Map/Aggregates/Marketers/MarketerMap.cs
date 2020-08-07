using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Marketers
{
    public class MarketerMap : EntityTypeConfiguration<Marketer>
    {
        public MarketerMap()
        {
            HasKey(item => item.Id);
        }
    }
}