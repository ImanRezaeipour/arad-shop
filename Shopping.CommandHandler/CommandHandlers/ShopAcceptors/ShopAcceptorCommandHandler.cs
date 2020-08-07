using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.ShopAcceptors.Commands;
using Shopping.Commands.Commands.ShopAcceptors.Responses;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.DomainModel.Aggregates.ShopAcceptors.Aggregates;
using Shopping.DomainModel.Aggregates.ShopAcceptors.ValueObjects;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.ShopAcceptors
{
    public class ShopAcceptorCommandHandler : ICommandHandler<AcceptShopAcceptorCommand, AcceptShopAcceptorCommandResponse>
        , ICommandHandler<RejectShopAcceptorCommand, RejectShopAcceptorCommandResponse>
        , ICommandHandler<CreateShopAcceptorCommand, CreateShopAcceptorCommandResponse>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<ShopAcceptor> _repository;
        public ShopAcceptorCommandHandler(IRepository<City> cityRepository, IRepository<ShopAcceptor> repository)
        {
            _cityRepository = cityRepository;
            _repository = repository;
        }

        public async Task<AcceptShopAcceptorCommandResponse> Handle(AcceptShopAcceptorCommand command)
        {
            var shopAcceptorAddress = await _repository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.Id);
            if (shopAcceptorAddress == null)
            {
                throw new DomainException("درخواست ثبت فروشگاه یافت نشد");
            }
            shopAcceptorAddress.ShopAcceptorStatus = ShopAcceptorStatus.Accept;
            return new AcceptShopAcceptorCommandResponse();
        }

        public async Task<RejectShopAcceptorCommandResponse> Handle(RejectShopAcceptorCommand command)
        {
            var shopAcceptorAddress = await _repository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.Id);
            if (shopAcceptorAddress == null)
            {
                throw new DomainException("درخواست ثبت فروشگاه یافت نشد");
            }
            shopAcceptorAddress.ShopAcceptorStatus = ShopAcceptorStatus.Reject;
            return new RejectShopAcceptorCommandResponse();
        }

        public async Task<CreateShopAcceptorCommandResponse> Handle(CreateShopAcceptorCommand command)
        {
            var city = await _cityRepository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.ShopAcceptorAddress.CityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }
            var userInfo = new UserInfo(command.UserInfo.UserId, command.UserInfo.FirstName,
                command.UserInfo.LastName);
            var shopAcceptorAddress = new ShopAcceptorAddress(command.ShopAcceptorAddress.AddressText,
                 command.ShopAcceptorAddress.CityId,
                city.CityName);
            var shopAcceptor = new ShopAcceptor(Guid.NewGuid(), command.FirstName, command.LastName,
                command.PhoneNumber, command.MobileNumber, command.ShopName, userInfo, shopAcceptorAddress);
            _repository.Add(shopAcceptor);
            return new CreateShopAcceptorCommandResponse();
        }
    }
}