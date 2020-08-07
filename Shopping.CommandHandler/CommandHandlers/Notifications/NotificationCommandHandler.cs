using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Notifications.Commands;
using Shopping.Commands.Commands.Notifications.Responses;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates.Abstract;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Notifications
{
    public class NotificationCommandHandler
    : ICommandHandler<VisitedPanelNotificationCommand, NotificationCommandResponse>
    {
        private readonly IRepository<NotificationBase> _repository;
        public NotificationCommandHandler(IRepository<NotificationBase> repository)
        {
            _repository = repository;
        }
        public async Task<NotificationCommandResponse> Handle(VisitedPanelNotificationCommand command)
        {
            var notification = await _repository.AsQuery().OfType<PanelNotification>()
                .SingleOrDefaultAsync(item => item.Id == command.Id);
            if (notification == null)
            {
                throw new DomainException("پیام یافت نشد");
            }
            notification.Visited();
            return new NotificationCommandResponse();
        }
    }
}