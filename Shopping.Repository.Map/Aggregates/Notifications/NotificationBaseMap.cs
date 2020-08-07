using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates.Abstract;

namespace Shopping.Repository.Map.Aggregates.Notifications
{
    public class NotificationBaseMap : EntityTypeConfiguration<NotificationBase>
    {
        public NotificationBaseMap()
        {
            HasKey(item => item.Id);
        }
    }
}