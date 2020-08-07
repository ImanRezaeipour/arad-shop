using AutoMapper;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates.Abstract;
using Shopping.QueryModel.QueryModels.Notifications;

namespace Shopping.QueryService.Implements.Notifications
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationBase, INotificationBaseDto>()
                .Include<PanelNotification, IPanelNotificationDto>();
            CreateMap<PanelNotification, IPanelNotificationDto>();
        }
    }
}