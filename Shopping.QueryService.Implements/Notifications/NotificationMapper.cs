using AutoMapper;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates;
using Shopping.QueryModel.QueryModels.Notifications;

namespace Shopping.QueryService.Implements.Notifications
{
    public static class NotificationMapper
    {
        public static IPanelNotificationDto ToDto(this PanelNotification src)
        {
            return Mapper.Map<IPanelNotificationDto>(src);
        }
    }
}