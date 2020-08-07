using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Notifications
{
    public interface IPanelNotificationDto : INotificationBaseDto
    {
        PanelNotificationType Type { get; set; }
        bool IsVisited { get; set; }
        string AdditionalData { get; set; }
    }
}