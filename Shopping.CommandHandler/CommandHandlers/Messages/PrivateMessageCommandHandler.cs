using System;
using System.Linq;
using System.Threading.Tasks;
using Kernel.Notify.Message.Interfaces;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Messages.Commands;
using Shopping.Commands.Commands.Messages.Responses;
using Shopping.DomainModel.Aggregates.Messages.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;

namespace Shopping.CommandHandler.CommandHandlers.Messages
{
    public class PrivateMessageCommandHandler : ICommandHandler<CreatePrivateMessageCommand, CreatePrivateMessageCommandResponse>
    {
        private readonly IRepository<PrivateMeassge> _repository;
        private readonly IRepository<Person> _personRepository;
        private readonly IFcmNotification _fcmNotification;
        public PrivateMessageCommandHandler(IRepository<PrivateMeassge> repository, IRepository<Person> personRepository, IFcmNotification fcmNotification)
        {
            _repository = repository;
            _personRepository = personRepository;
            _fcmNotification = fcmNotification;
        }
        public async Task<CreatePrivateMessageCommandResponse> Handle(CreatePrivateMessageCommand command)
        {
            var person = _personRepository.AsQuery().SingleOrDefault(p => p.Id == command.PersonId);
            if (person == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var privateMessage = new PrivateMeassge(Guid.NewGuid(), command.Title, command.Body,
                new UserInfo(command.UserInfo.UserId, command.UserInfo.FirstName, command.UserInfo.LastName),
                command.PersonId);
            _repository.Add(privateMessage);
            var appType = person is Customer ? AppType.Customer : AppType.Shop;
            await _fcmNotification.SendToIds(person.GetPushTokens(), command.Title, command.Body,
                NotificationType.PrivateMessage, appType, NotificationSound.Default);
            return new CreatePrivateMessageCommandResponse();
        }
    }
}