using System;
using System.Threading.Tasks;
using Kernel.Notify.Message.Interfaces;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Messages.Commands;
using Shopping.Commands.Commands.Messages.Responses;
using Shopping.DomainModel.Aggregates.Messages.Aggregates;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;

namespace Shopping.CommandHandler.CommandHandlers.Messages
{
    public class PublicMessageCommandHandler : ICommandHandler<CreatePublicMessageCommand, CreatePublicMessageCommandResponse>
    {
        private readonly IRepository<PublicMessage> _repository;
        private readonly IFcmNotification _fcmNotification;
        public PublicMessageCommandHandler(IRepository<PublicMessage> repository, IFcmNotification fcmNotification)
        {
            _repository = repository;
            _fcmNotification = fcmNotification;
        }
        public async Task<CreatePublicMessageCommandResponse> Handle(CreatePublicMessageCommand command)
        {
            var publicMessage = new PublicMessage(Guid.NewGuid(), command.Title, command.Body, new UserInfo(command.UserInfo.UserId, command.UserInfo.FirstName, command.UserInfo.LastName), command.MessageType);
            _repository.Add(publicMessage);
            await _fcmNotification.SendToTopic(command.Title, command.Body, NotificationType.PublicMessage,
                   command.MessageType == PublicMessageType.ShopMessage ? AppType.Shop : AppType.Customer, NotificationSound.Default);
            return new CreatePublicMessageCommandResponse();
        }
    }
}