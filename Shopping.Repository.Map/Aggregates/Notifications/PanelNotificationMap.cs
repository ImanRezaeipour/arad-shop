using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Notifications
{
    public class PanelNotificationMap : EntityTypeConfiguration<PanelNotification>
    {
        public PanelNotificationMap()
        {
            ToTable("PanelNotification");
        }
    }
}