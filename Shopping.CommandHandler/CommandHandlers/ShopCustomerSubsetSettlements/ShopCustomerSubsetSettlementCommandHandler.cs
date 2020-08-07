//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Shopping.AsyncCommanBus.Handling;
//using Shopping.Commands.Commands.ShopCustomerSubsetSettlement.Commands;
//using Shopping.Commands.Commands.ShopCustomerSubsetSettlement.Responses;
//using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
//using Shopping.DomainModel.Aggregates.Persons.Aggregates;
//using Shopping.DomainModel.Aggregates.Persons.Events;
//using Shopping.DomainModel.Aggregates.Shared;
//using Shopping.DomainModel.Aggregates.ShopCustomerSubsetSettlements.Aggregates;
//using Shopping.Infrastructure.Core;
//using Shopping.Infrastructure.Core.DomainEvent;
//using Shopping.Repository.Write.Interface;

//namespace Shopping.CommandHandler.CommandHandlers.ShopCustomerSubsetSettlements
//{
//    public class ShopCustomerSubsetSettlementCommandHandler :
//        ICommandHandler<CreateShopCustomerSubsetSettlementCommand, ShopCustomerSubsetSettlementCommandResponse>
//    {
//        private readonly IRepository<ShopCustomerSubsetSettlement> _repository;
//        private readonly IRepository<Shop> _shopRepository;
//        private readonly IRepository<ApplicationSetting> _applicationSettingRepository;
//        public ShopCustomerSubsetSettlementCommandHandler(
//            IRepository<ShopCustomerSubsetSettlement> repository,
//            IRepository<Shop> shopRepository,
//            IRepository<ApplicationSetting> applicationSettingRepository)
//        {
//            _repository = repository;
//            _shopRepository = shopRepository;
//            _applicationSettingRepository = applicationSettingRepository;
//        }
//        public async Task<ShopCustomerSubsetSettlementCommandResponse> Handle(CreateShopCustomerSubsetSettlementCommand command)
//        {
//            var shop = await _shopRepository.FindAsync(command.ShopId);
//            if (shop == null)
//            {
//                throw new DomainException("فروشگاه یافت نشد");
//            }
//            var appSetting = _applicationSettingRepository.AsQuery().FirstOrDefault();

//            var sumShopCustomerSubsetNotSettlement =
//                shop.CustomerSubsets.Count(p => !p.IsSettlement && !p.HavePaidFactor) *
//                appSetting.ShopCustomerSubsetAmount;

//            var sumShopCustomerSubsetHaveFactorPaidSumAmount =
//                shop.CustomerSubsets.Count(p => !p.IsSettlement && p.HavePaidFactor) *
//                appSetting.ShopCustomerSubsetHaveFactorPaidAmount;
//            if (sumShopCustomerSubsetNotSettlement + sumShopCustomerSubsetHaveFactorPaidSumAmount == 0)
//            {
//                throw new DomainException("مبلغی برای تسویه وجود ندارد");
//            }
//            var shopCustomerSubsetSettlement = new ShopCustomerSubsetSettlement(Guid.NewGuid(), shop, new UserInfo(command.UserInfo.UserId, command.UserInfo.FirstName, command.UserInfo.LastName),
//                sumShopCustomerSubsetNotSettlement + sumShopCustomerSubsetHaveFactorPaidSumAmount);

//            _repository.Add(shopCustomerSubsetSettlement);

//            DomainEventDispatcher.Raise(new SetShopCustomerSubsetSettlementEvent(shop));

//            return new ShopCustomerSubsetSettlementCommandResponse();
//        }
//    }
//}