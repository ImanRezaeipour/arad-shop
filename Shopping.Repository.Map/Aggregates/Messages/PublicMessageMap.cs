using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Messages.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Messages
{
    public class PublicMessageMap:EntityTypeConfiguration<PublicMessage>
    {
        public PublicMessageMap()
        {
            Map(p =>
            {
                p.MapInheritedProperties();
                p.ToTable("PublicMessage");
            });
        }
    }
}