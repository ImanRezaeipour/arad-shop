using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Messages.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Messages
{
    public class PrivateMessageMap:EntityTypeConfiguration<PrivateMeassge>
    {
        public PrivateMessageMap()
        {
            Map(p =>
            {
                p.MapInheritedProperties();
                p.ToTable("PrivateMessage");
            });
        }
    }
}