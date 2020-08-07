using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.QueryModel.QueryModels.Notifications;

namespace Shopping.QueryService.Interfaces.Notifications
{
    public interface INotificationQueryService
    {
        Task<IList<IPanelNotificationDto>> GetUnSeenPanelNotification();
    }
}