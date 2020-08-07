using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kernel.Notify.Message.Interfaces;
using MassTransit;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Interfaces;
using Shopping.DomainModel.Aggregates.Notifications.Events;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Entities;
using Shopping.DomainModel.Aggregates.Persons.Interfaces;
using Shopping.DomainModel.Aggregates.Persons.ValueObjects;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Events;
using Shopping.Infrastructure;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.Helper;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;
using Shopping.Shared.Events.Interfaces.Users;
using Shopping.Shared.Events.Messages.Users;

namespace Shopping.CommandHandler.CommandHandlers.Persons
{
    public class ShopCommandHandler : ICommandHandler<AcceptShopCommand, AcceptShopCommandResponse>
        , ICommandHandler<NeedToModifyShopCommand, NeedToModifyShopCommandResponse>
        , ICommandHandler<RejectShopCommand, RejectShopCommandResponse>
        , ICommandHandler<CreateShopCommand, CreateShopCommandResponse>
        , ICommandHandler<UpdateShopCommand, UpdateShopCommandResponse>
        , ICommandHandler<DeActiveShopCommand, DeActiveShopCommandResponse>
        , ICommandHandler<ActiveShopCommand, ActiveShopCommandResponse>
    {
        private readonly IBus _eventBus;
        private readonly IFcmNotification _fcmNotification;
        private readonly IRepository<Shop> _repository;
        private readonly IMarketerDomainService _marketerDomainService;
        private readonly ISeqRepository _seqRepository;
        private readonly IRepository<Marketer> _marketerRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IPersonDomainService _personDomainService;
        private readonly IRepository<ApplicationSetting> _applicationSettingRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IContext _context;
        public ShopCommandHandler(IRepository<City> cityRepository,
            IRepository<Shop> repository,
            IPersonDomainService personDomainService,
            IFcmNotification fcmNotification,
            IRepository<ApplicationSetting> applicationSettingRepository,
            IRepository<Marketer> marketerRepository,
            IMarketerDomainService marketerDomainService,
            ISeqRepository seqRepository,
            IRepository<Province> provinceRepository,
            IBus eventBus,
            IContext context)
        {
            _repository = repository;
            _personDomainService = personDomainService;
            _fcmNotification = fcmNotification;
            _applicationSettingRepository = applicationSettingRepository;
            _marketerRepository = marketerRepository;
            _marketerDomainService = marketerDomainService;
            _seqRepository = seqRepository;
            _provinceRepository = provinceRepository;
            _eventBus = eventBus;
            _context = context;
            _cityRepository = cityRepository;
        }
        public async Task<AcceptShopCommandResponse> Handle(AcceptShopCommand command)
        {
            var shop = _repository.Find(command.Id);
            if (shop == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var shopStatusLog = new ShopStatusLog(Guid.NewGuid(), command.UserId, command.FirstName, command.LastName, ShopStatus.Accept);
            shop.ShopStatus = ShopStatus.Accept;
            shop.DescriptionStatus = command.DescriptionStatus;
            shop.ShopStatusLogs.Add(shopStatusLog);
            await _fcmNotification.SendToIds(shop.GetPushTokens(), "تایید فروشگاه", "تایید فروشگاه با موفقیت انجام شد",
                NotificationType.ShopActivated, AppType.Shop, NotificationSound.Shopper);
            return new AcceptShopCommandResponse();
        }
        public async Task<RejectShopCommandResponse> Handle(RejectShopCommand command)
        {
            var shop = _repository.Find(command.Id);
            if (shop == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            var shopStatusLog = new ShopStatusLog(Guid.NewGuid(), command.UserId, command.FirstName, command.LastName, ShopStatus.Reject);
            shop.ShopStatus = ShopStatus.Reject;
            shop.DescriptionStatus = command.DescriptionStatus;
            shop.ShopStatusLogs.Add(shopStatusLog);
            await _fcmNotification.SendToIds(shop.GetPushTokens(), "رد تایید فروشگاه", "رد تایید فروشگاه با موفقیت انجام شد",
                   NotificationType.ShopActivated, AppType.Shop, NotificationSound.Shopper);
            return new RejectShopCommandResponse();
        }
        public async Task<DeActiveShopCommandResponse> Handle(DeActiveShopCommand command)
        {
            var shop = await _repository.FindAsync(command.Id);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            shop.DeActive();
            _context.SaveChanges();
            await _eventBus.Publish<IDeActiveUserEvent>(new DeActiveUserEvent(shop.UserId, AppType.Shop));
            return new DeActiveShopCommandResponse();
        }
        public async Task<ActiveShopCommandResponse> Handle(ActiveShopCommand command)
        {
            var shop = await _repository.FindAsync(command.Id);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            shop.Active();
            _context.SaveChanges();
            await _eventBus.Publish<IActiveUserEvent>(new ActiveUserEvent(shop.UserId, AppType.Shop));
            return new ActiveShopCommandResponse();
        }
        public async Task<CreateShopCommandResponse> Handle(CreateShopCommand command)
        {
            var shopNumber = _seqRepository.GetNextSequenceValue(SqNames.ShopNumberSequence);
            var marketer = await _marketerRepository.AsQuery().SingleOrDefaultAsync(p => p.BarcodeId == command.BarcodeId);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            _marketerDomainService.CheckMarketerActive(marketer);
            _marketerDomainService.CheckMaxMarketerAllowedIsEnough(marketer);
            _personDomainService.CheckShopIsExist(command.UserId);
            var city = _cityRepository.Find(command.Address.CityId);
            if (city == null)
            {
                throw new DomainException("شهر وارد شده یافت نشد");
            }
            var appSetting = await _applicationSettingRepository.AsQuery().SingleOrDefaultAsync();
            if (appSetting == null)
            {
                throw new DomainException("تنظیمات برنامه یافت نشد");
            }
            if (command.DefaultDiscount < appSetting.MinimumDiscount || command.DefaultDiscount > appSetting.MaximumDiscount)
            {
                throw new DomainException("تخفیف پیش فرض در بازه تخفیفات معتبر نمی باشد");
            }
            var address = new ShopAddress(city.Id, city.Code, city.CityName, command.Address.AddressText,
                command.Address.PhoneNumber, command.Address.Position.ToDbGeography(),
                command.Address.ShopMobileNumber, command.Address.ZoneId,
                city.Province.Id, city.Province.Code, city.Province.Name);
            var accountBank = new BankAccount(command.BankAccount.Iban, command.BankAccount.AccountOwnerName, command.BankAccount.AccountNumber);
            var imageDocument = new ImageDocuments(command.ImageDocuments.FaceImage, command.ImageDocuments.NationalCardImage);
            var shop = new Shop(Guid.NewGuid(), command.Name, command.FirstName, command.LastName, command.EmailAddress,
                command.UserId, command.Description, command.NationalCode, address, accountBank, imageDocument,
                command.MobileNumber, command.AreaRadius, command.Metrage, command.DefaultDiscount, marketer.Id, shopNumber)
            {
                AppInfos = new List<AppInfo>(),
                CustomerSubsets = new List<ShopCustomerSubset>()
            };
            _repository.Add(shop);
            DomainEventDispatcher.Raise(new AssignmentShopMarketersHistoryEvent(shop, marketer,
                new UserInfo(command.UserId, command.MobileNumber, "کاربر موبایل")));
            DomainEventDispatcher.Raise(new AddPanelNotificationEvent(Guid.NewGuid(), command.Name, "فروشگاه ایجاد شد",
                PanelNotificationType.ShopCreated, shop.Id.ToString()));
            return new CreateShopCommandResponse();
        }
        public async Task<NeedToModifyShopCommandResponse> Handle(NeedToModifyShopCommand command)
        {
            var shop = _repository.Find(command.Id);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var shopStatusLog = new ShopStatusLog(Guid.NewGuid(), command.UserId, command.FirstName, command.LastName, ShopStatus.NeedToModify);
            shop.ShopStatus = ShopStatus.NeedToModify;
            shop.DescriptionStatus = command.DescriptionStatus;
            shop.ShopStatusLogs.Add(shopStatusLog);
            await _fcmNotification.SendToIds(shop.GetPushTokens(), "نیاز به اصلاح اطلاعات", "نیاز به اصلاح اطلاعات فروشگاه با موفقیت انجام شد",
                NotificationType.ShopActivated, AppType.Shop, NotificationSound.Shopper);
            return new NeedToModifyShopCommandResponse();
        }
        public async Task<UpdateShopCommandResponse> Handle(UpdateShopCommand command)
        {
            var shop = await _repository.FindAsync(command.ShopId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            shop.AreaRadius = command.AreaRadius;
            shop.Description = command.Description;
            shop.Metrage = command.Metrage;
            shop.Name = command.Name;
            shop.NationalCode = command.NationalCode;
            shop.FirstName = command.FirstName;
            shop.LastName = command.LastName;
            shop.EmailAddress = command.EmailAddress;
            shop.BankAccount = new BankAccount(command.BankAccount.Iban, command.BankAccount.AccountOwnerName,
                command.BankAccount.AccountNumber);
            shop.ImageDocuments =
                new ImageDocuments(command.ImageDocuments.FaceImage, command.ImageDocuments.NationalCardImage);
            var province = _provinceRepository.Find(command.ShopAddress.ProvinceId);
            if (province == null)
            {
                throw new DomainException("استان یافت نشد");
            }

            var city = province.Cities.SingleOrDefault(item => item.Id == command.ShopAddress.CityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }

            var zone = city.Zones.SingleOrDefault(item => item.Id == command.ShopAddress.ZoneId);
            if (zone == null)
            {
                throw new DomainException("منطقه یافت نشد");
            }

            shop.ShopAddress = new ShopAddress(city.Id, city.Code, city.CityName, command.ShopAddress.AddressText,
                command.ShopAddress.PhoneNumber, command.ShopAddress.Position.ToDbGeography(),
                command.ShopAddress.ShopMobileNumber, zone.Id, province.Id, province.Code, province.Name);

            return new UpdateShopCommandResponse();
        }
    }
}