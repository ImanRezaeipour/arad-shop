using Microsoft.AspNet.SignalR;

namespace Shopping.SignalR.Hubs
{
    public class ShoppingHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.ShopCreationNotification(message);
        }
    }
}