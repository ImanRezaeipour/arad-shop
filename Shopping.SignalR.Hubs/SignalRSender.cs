using Microsoft.AspNet.SignalR;
using Shopping.SignalR.Hubs.Messages;

namespace Shopping.SignalR.Hubs
{
    public class SignalRSender
    {
        public static void NotifyShopCreated(PanelNotificationMessage message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ShoppingHub>();
            hubContext.Clients.All.ShopCreationNotification(message);
        }
    }
}