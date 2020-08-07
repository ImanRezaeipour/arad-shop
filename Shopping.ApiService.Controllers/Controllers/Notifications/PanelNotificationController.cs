using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Notifications.Commands;
using Shopping.Commands.Commands.Notifications.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.Notifications;

namespace Shopping.ApiService.Controllers.Controllers.Notifications
{
    [Authorize]
    [RoutePrefix("api/PanelNotification")]
    public class PanelNotificationController : ApiControllerBase
    {
        private readonly INotificationQueryService _notificationQueryService;
        public PanelNotificationController(ICommandBus bus, INotificationQueryService notificationQueryService) : base(bus)
        {
            _notificationQueryService = notificationQueryService;
        }
        public async Task<IHttpActionResult> Get()
        {
            var result = await _notificationQueryService.GetUnSeenPanelNotification();
            return Ok(result);
        }
        [Route("Visited")]
        public async Task<IHttpActionResult> Put(VisitedPanelNotificationCommand command)
        {
            var response =
                await Bus.Send<VisitedPanelNotificationCommand, NotificationCommandResponse>(command);
            return Ok(response);
        }
    }
}