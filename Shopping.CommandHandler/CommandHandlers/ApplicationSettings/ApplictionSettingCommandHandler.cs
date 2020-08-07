using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.ApplicationSettings.Commands;
using Shopping.Commands.Commands.ApplicationSettings.Responses;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Interfaces;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.ApplicationSettings
{
    public class ApplicationSettingCommandHandler :
        ICommandHandler<UpdateApplicationSettingCommand, UpdateApplicationSettingCommandResponse>
    {
        private readonly IApplicationSettingDomainService _applicationSettingDomainService;

        private readonly IRepository<DomainModel.Aggregates.ApplicationSettings.Aggregates.ApplicationSetting>
            _repository;
        public ApplicationSettingCommandHandler(IApplicationSettingDomainService applicationSettingDomainService, IRepository<DomainModel.Aggregates.ApplicationSettings.Aggregates.ApplicationSetting> repository)
        {
            _applicationSettingDomainService = applicationSettingDomainService;
            _repository = repository;
        }

        public async Task<UpdateApplicationSettingCommandResponse> Handle(UpdateApplicationSettingCommand command)
        {
            _applicationSettingDomainService.CheckDiscount(command.MinimumDiscount, command.MaximumDiscount);
            var existApplicationSetting = await _repository.AsQuery().FirstOrDefaultAsync();
            if (existApplicationSetting == null)
            {
                var applicationSetting =
                    new DomainModel.Aggregates.ApplicationSettings.Aggregates.ApplicationSetting(Guid.NewGuid(),
                        command.MinimumDiscount, command.MaximumDiscount,
                        command.MaximumDeliveryTime, command.OrderSuggestionExpireTime, command.MinimumBuy,
                        command.ShopAppVersion,
                        command.ShopDownloadLink, command.CustomerAppVersion, command.CustomerDownloadLink
                        , command.ShopAppVersionIos, command.ShopDownloadLinkIos, command.CustomerAppVersionIos,
                        command.CustomerDownloadLinkIos, command.FactorExpireTime, command.IosStoreCheck,
                        command.ShopCustomerSubsetAmount, command.ShopCustomerSubsetHaveFactorPaidAmount,
                        command.ShopCustomerSubsetHaveFactorPaidCount,
                        command.RecommendedSystemIsActive, command.CustomerRequestOrderDuration,
                        command.CustomerRequestOrderCount,
                        command.OrderItemResponseTime, command.OrderExpireOpenTime);
                _repository.Add(applicationSetting);
            }
            else
            {
                existApplicationSetting.OrderItemResponseTime = command.OrderItemResponseTime;
                existApplicationSetting.OrderExpireOpenTime = command.OrderExpireOpenTime;
                existApplicationSetting.CustomerRequestOrderDuration = command.CustomerRequestOrderDuration;
                existApplicationSetting.CustomerRequestOrderCount = command.CustomerRequestOrderCount;
                existApplicationSetting.MaximumDeliveryTime = command.MaximumDeliveryTime;
                existApplicationSetting.MaximumDiscount = command.MaximumDiscount;
                existApplicationSetting.MinimumBuy = command.MinimumBuy;
                existApplicationSetting.MinimumDiscount = command.MinimumDiscount;
                existApplicationSetting.OrderSuggestionExpireTime = command.OrderSuggestionExpireTime;
                existApplicationSetting.ShopDownloadLink = command.ShopDownloadLink;
                existApplicationSetting.ShopAppVersion = command.ShopAppVersion;
                existApplicationSetting.CustomerDownloadLink = command.CustomerDownloadLink;
                existApplicationSetting.CustomerAppVersion = command.CustomerAppVersion;
                existApplicationSetting.FactorExpireTime = command.FactorExpireTime;
                existApplicationSetting.ShopDownloadLinkIos = command.ShopDownloadLinkIos;
                existApplicationSetting.ShopAppVersionIos = command.ShopAppVersionIos;
                existApplicationSetting.CustomerDownloadLinkIos = command.CustomerDownloadLinkIos;
                existApplicationSetting.CustomerAppVersionIos = command.CustomerAppVersionIos;
                existApplicationSetting.IosStoreCheck = command.IosStoreCheck;
                existApplicationSetting.ShopCustomerSubsetAmount = command.ShopCustomerSubsetAmount;
                existApplicationSetting.ShopCustomerSubsetHaveFactorPaidAmount = command.ShopCustomerSubsetHaveFactorPaidAmount;
                existApplicationSetting.ShopCustomerSubsetHaveFactorPaidCount = command.ShopCustomerSubsetHaveFactorPaidCount;
                existApplicationSetting.RecommendedSystemIsActive = command.RecommendedSystemIsActive;
            }
            return new UpdateApplicationSettingCommandResponse();
        }
    }
}