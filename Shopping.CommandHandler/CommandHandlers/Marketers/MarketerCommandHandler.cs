using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Marketers.Commands;
using Shopping.Commands.Commands.Marketers.Responses;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Entities;
using Shopping.DomainModel.Aggregates.Marketers.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Events;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Marketers
{
    public class MarketerCommandHandler :
        ICommandHandler<CreateMarketerCommand, MarketerCommandResponse>
        ,ICommandHandler<UpdateMarketerCommand, UpdateMarketerCommandResponse>
    , ICommandHandler<DeActiveMarketerCommand, MarketerCommandResponse>
    , ICommandHandler<ActiveMarketerCommand, MarketerCommandResponse>
    , ICommandHandler<AddMarketerCommentCommand, MarketerCommandResponse>
    , ICommandHandler<DetachedMarketerShopCommand, MarketerCommandResponse>
    , ICommandHandler<ChangeShopMarketerCommand, MarketerCommandResponse>
    {
        private readonly IRepository<Marketer> _repository;
        private readonly IRepository<Shop> _shopRepository;
        public MarketerCommandHandler(IRepository<Marketer> repository,
            IRepository<Shop> shopRepository)
        {
            _repository = repository;
            _shopRepository = shopRepository;
        }
        public Task<MarketerCommandResponse> Handle(CreateMarketerCommand command)
        {
            var marketerAddress = new MarketerAddress(command.MarketerAddress.AddressText,
                command.MarketerAddress.PhoneNumber, command.MarketerAddress.MobileNumber,
                command.MarketerAddress.RequiredPhoneNumber, command.MarketerAddress.CityId,
                command.MarketerAddress.CityName);
            var marketerSalary = new MarketerSalary(command.MarketerSalary.FixedSalary, command.MarketerSalary.InterestRates);
            var marketerReagent = new MarketerReagent(command.MarketerReagent.ReagentName,
                command.MarketerReagent.ReagentMobileNumber);
            var marketer = new Marketer(command.BarcodeId, command.FirstName, command.LastName, command.NationalCode,
                command.IdNumber, command.Gender, command.FatherName, command.BirthDate, command.Description,
                command.MaxMarketerAllowed, command.Documents, command.Image, marketerAddress, marketerSalary,
                marketerReagent, command.UserId)
            {
                Comments = new List<MarketerComment>()
            };
            _repository.Add(marketer);
            return Task.FromResult(new MarketerCommandResponse());
        }
        public async Task<MarketerCommandResponse> Handle(DeActiveMarketerCommand command)
        {
            var marketer = await _repository.FindAsync(command.Id);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            marketer.DeActive();
            return new MarketerCommandResponse();
        }
        public async Task<MarketerCommandResponse> Handle(ActiveMarketerCommand command)
        {
            var marketer = await _repository.FindAsync(command.Id);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            marketer.Active();
            return new MarketerCommandResponse();
        }
        public async Task<MarketerCommandResponse> Handle(AddMarketerCommentCommand command)
        {
            var marketer = await _repository.FindAsync(command.MarketerId);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            marketer.Comments.Add(new MarketerComment(Guid.NewGuid(), command.CommentTitle));
            return new MarketerCommandResponse();
        }
        public async Task<MarketerCommandResponse> Handle(DetachedMarketerShopCommand command)
        {
            var marketer = await _repository.FindAsync(command.MarketerId);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            var shop = await _shopRepository.FindAsync(command.ShopId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            shop.MarketerId = null;
            var userInfo = new UserInfo(command.UserInfo.UserId, command.UserInfo.FirstName, command.UserInfo.LastName);
            DomainEventDispatcher.Raise(new DetachedShopMarketersHistoryEvent(shop, marketer, userInfo));
            return new MarketerCommandResponse();
        }
        public async Task<MarketerCommandResponse> Handle(ChangeShopMarketerCommand command)
        {
            var shop = await _shopRepository.FindAsync(command.ShopId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var marketer = await _repository.FindAsync(shop.MarketerId);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            var newMarketer = await _repository.FindAsync(command.NewMarketerId);
            if (newMarketer == null)
            {
                throw new DomainException("بازاریاب جدید یافت نشد");
            }
            shop.MarketerId = newMarketer.Id;
            if (newMarketer.Equals(marketer))
            {
                throw new DomainException("بازایاب انتخابی شما با بازاریاب کنونی یکی است");
            }
            DomainEventDispatcher.Raise(new DetachedShopMarketersHistoryEvent(shop, marketer,
                new UserInfo(command.UserInfo.UserId, command.UserInfo.FirstName, command.UserInfo.LastName)));
            DomainEventDispatcher.Raise(new AssignmentShopMarketersHistoryEvent(shop, marketer,
                new UserInfo(command.UserInfo.UserId, command.UserInfo.FirstName, command.UserInfo.LastName)));
            return new MarketerCommandResponse();
        }

        public Task<UpdateMarketerCommandResponse> Handle(UpdateMarketerCommand command)
        {
            var marketer = _repository.AsQuery().SingleOrDefault(p => p.Id == command.Id);
            if (marketer== null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            var countShopMarketer= _shopRepository.AsQuery().Count(p => p.MarketerId == command.Id);
            if (countShopMarketer >command.MaxMarketerAllowed)
            {
             throw new DomainException("تعداد فروشگاه این یازاریاب بیش از تعداد مجاز وارد شده می باشد");
            }
            var marketerAddress = new MarketerAddress(command.MarketerAddress.AddressText,
                command.MarketerAddress.PhoneNumber, command.MarketerAddress.MobileNumber,
                command.MarketerAddress.RequiredPhoneNumber, command.MarketerAddress.CityId,
                command.MarketerAddress.CityName);
            var marketerSalary = new MarketerSalary(command.MarketerSalary.FixedSalary, command.MarketerSalary.InterestRates);
            var marketerReagent = new MarketerReagent(command.MarketerReagent.ReagentName,
                command.MarketerReagent.ReagentMobileNumber);
            marketer.MarketerAddress = marketerAddress;
            marketer.MarketerSalary = marketerSalary;
            marketer.MarketerReagent = marketerReagent;
            marketer.BarcodeId = command.BarcodeId;
            marketer.FatherName = command.FatherName;
            marketer.LastName = command.LastName;
            marketer.FirstName = command.FirstName;
            marketer.NationalCode = command.NationalCode;
            marketer.IdNumber = command.IdNumber;
            marketer.Gender = command.Gender;
            marketer.BirthDate = command.BirthDate;
            marketer.MaxMarketerAllowed = command.MaxMarketerAllowed;
            marketer.Documents = "";
            marketer.Documents = string.Join(",", command.Documents);
            marketer.Image = command.Image;
           
            return Task.FromResult(new UpdateMarketerCommandResponse());
        }
    }
}