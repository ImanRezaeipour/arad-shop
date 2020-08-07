using System;
using System.Linq;
using Shopping.DomainModel.Aggregates.Messages.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements;
using Shopping.QueryModel.Implements.Messages;
using Shopping.QueryModel.QueryModels.Messages;
using Shopping.QueryService.Interfaces.Messages;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Messages
{
    public class MessageQueryService : IMessageQueryService
    {
        private readonly IReadOnlyRepository<PublicMessage, Guid> _publicMessageRepository;
        private readonly IReadOnlyRepository<PrivateMeassge, Guid> _privateMessageRepository;
        private readonly IReadOnlyRepository<Person, Guid> _personRepository;
        public MessageQueryService(IReadOnlyRepository<PublicMessage, Guid> publicMessageRepository, IReadOnlyRepository<PrivateMeassge, Guid> privateMessageRepository, IReadOnlyRepository<Person, Guid> personId)
        {
            _publicMessageRepository = publicMessageRepository;
            _privateMessageRepository = privateMessageRepository;
            _personRepository = personId;
        }
        public IQueryable<IPublicMessageDto> GetAllPublicMessage()
        {
            var result = _publicMessageRepository.AsQuery()
                .OrderByDescending(p => p.CreationTime)
                .Select(item => new PublicMessageDto
                {
                    Body = item.Body,
                    CreationTime = item.CreationTime,
                    PublicMessageType = item.PublicMessageType,
                    Title = item.Title,
                    UserInfo = new UserInfoDto
                    {
                        UserId = item.UserInfo.UserId,
                        FirstName = item.UserInfo.FirstName,
                        LastName = item.UserInfo.LastName
                    }
                });
            return result;
        }
        public IQueryable<IPrivateMessageDto> GetAllPrivateMessageByPersonId(Guid personId)
        {
            var result = _privateMessageRepository.AsQuery()
                .Where(p => p.PersonId == personId)
                .OrderByDescending(p => p.CreationTime)
                    .Select(item => new PrivateMessageDto
                    {
                        Body = item.Body,
                        CreationTime = item.CreationTime,
                        Title = item.Title,
                        PersonId = item.PersonId,
                        UserInfo = new UserInfoDto
                        {
                            UserId = item.UserInfo.UserId,
                            FirstName = item.UserInfo.FirstName,
                            LastName = item.UserInfo.LastName
                        }
                    });
            return result;
        }
        public MobilePagingResultDto<IPrivateMessageDto> GetCustomerPrivateMessageByUserId(Guid userId, PagedInputDto pagedInput)
        {
            var person = _personRepository.AsQuery().OfType<Customer>().SingleOrDefault(p => p.UserId == userId);
            if (person == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var data = _privateMessageRepository.AsQuery()
                .Where(p => p.PersonId == person.Id);
            var result = data.OrderByDescending(p => p.CreationTime)
                    .Skip(pagedInput.Skip)
                    .Take(pagedInput.Count)
                    .ToList()
                    .Select(item => item.ToDto())
                    .ToList();
            return new MobilePagingResultDto<IPrivateMessageDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IPrivateMessageDto> GetShopPrivateMessageByUserId(Guid userId, PagedInputDto pagedInput)
        {
            var person = _personRepository.AsQuery().OfType<Shop>().SingleOrDefault(p => p.UserId == userId);
            if (person == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var data = _privateMessageRepository.AsQuery()
                .Where(p => p.PersonId == person.Id);
            var result = data.OrderByDescending(p => p.CreationTime)
                    .Skip(pagedInput.Skip)
                    .Take(pagedInput.Count)
                    .ToList()
                    .Select(item => item.ToDto())
                    .ToList();
            return new MobilePagingResultDto<IPrivateMessageDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IPublicMessageDto> GetCustomerPublicMessages(PagedInputDto pagedInput)
        {
            var data = _publicMessageRepository.AsQuery()
                .Where(p => p.PublicMessageType == PublicMessageType.CustomerMessage);
            var result = data.OrderByDescending(p => p.CreationTime)
                    .Skip(pagedInput.Skip)
                    .Take(pagedInput.Count)
                    .ToList()
                    .Select(item => item.ToDto())
                    .ToList();
            return new MobilePagingResultDto<IPublicMessageDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
        public MobilePagingResultDto<IPublicMessageDto> GetShopPublicMessages(PagedInputDto pagedInput)
        {
            var data = _publicMessageRepository.AsQuery()
                .Where(p => p.PublicMessageType == PublicMessageType.ShopMessage);
            var result = data.OrderByDescending(p => p.CreationTime)
                    .Skip(pagedInput.Skip)
                    .Take(pagedInput.Count)
                    .ToList()
                    .Select(item => item.ToDto())
                    .ToList();
            return new MobilePagingResultDto<IPublicMessageDto>
            {
                Count = data.Count(),
                Result = result
            };
        }
    }
}