using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Shared.Enums;

namespace Kernel.Notify.Message.Interfaces
{
    public interface IFcmNotification
    {
        Task SendToIds(List<NotifyMessage> notifyMessages, string title, string body, NotificationType notificationType, AppType appType, string sound, string additionalData = "");
        Task SendToTopic(string title, string body, NotificationType notificationType, AppType appType, string sound);
    }
}