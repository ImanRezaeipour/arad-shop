using System.Linq;
using System.Threading.Tasks;
using Kernel.Notify.Message.Interfaces;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Factors.Commands;
using Shopping.Commands.Commands.Factors.Responses;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;

namespace Shopping.CommandHandler.CommandHandlers.Factors
{
    public class FactorCommandHandler : ICommandHandler<ShipmentSendFactorCommand, ShipmentSendFactorCommandResponse>
        , ICommandHandler<ShipmentDeliveryFactorCommand, ShipmentDeliveryFactorCommandResponse>
    {
        private readonly IRepository<Factor> _repository;
        private readonly IFcmNotification _fcmNotification;
        public FactorCommandHandler(IRepository<Factor> repository, IFcmNotification fcmNotification)
        {
            _repository = repository;
            _fcmNotification = fcmNotification;
        }

        public async Task<ShipmentSendFactorCommandResponse> Handle(ShipmentSendFactorCommand command)
        {
            var factor = _repository.AsQuery().SingleOrDefault(p => p.Id == command.Id);
            if (factor == null)
            {
                throw new DomainException("فاکتور یافت نشد");
            }
            if (factor.FactorState == FactorState.Pending || factor.FactorState == FactorState.Failed)
            {
                throw new DomainException(" فاکتور پرداخت نشد است");
            }
            factor.SendShipment();
            await _fcmNotification.SendToIds(factor.Customer.GetPushTokens(),
                "Invoice",
               "Your invoice has been sent",
                NotificationType.SendFactor,
                AppType.Customer, NotificationSound.Shopper);
            return new ShipmentSendFactorCommandResponse();
        }
        public async Task<ShipmentDeliveryFactorCommandResponse> Handle(ShipmentDeliveryFactorCommand command)
        {
            var factor = _repository.AsQuery().SingleOrDefault(p => p.Id == command.Id);
            if (factor == null)
            {
                throw new DomainException("فاکتور یافت نشد");
            }
            if (factor.FactorState != FactorState.Paid)
            {
                throw new DomainException("فاکتور پرداخت نشد است");
            }
            factor.DeliveryShipment();
            await _fcmNotification.SendToIds(factor.Shop.GetPushTokens(),
                "Delivery invoice",
                "Invoice has been delivered to you",
                NotificationType.DeliveryFactor,
                AppType.Shop, NotificationSound.Shopper,
                factor.Id.ToString());
            return new ShipmentDeliveryFactorCommandResponse();
        }
    }
}