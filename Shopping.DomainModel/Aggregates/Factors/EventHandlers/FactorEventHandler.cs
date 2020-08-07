using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Entities;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Factors.Events;
using Shopping.DomainModel.Aggregates.Factors.Interfaces;
using Shopping.DomainModel.Aggregates.Factors.ValueObjects;
using Shopping.DomainModel.Aggregates.ShopOrderLogs.Events;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Repository.Write.Interface;

namespace Shopping.DomainModel.Aggregates.Factors.EventHandlers
{
    public class FactorEventHandler : IDomainEventHandler<CreateFactorEvent>
    {
        private readonly IRepository<Factor> _repository;
        private readonly IFactorDomainService _factorDomainService;
        public FactorEventHandler(IRepository<Factor> repository, IFactorDomainService factorDomainService)
        {
            _repository = repository;
            _factorDomainService = factorDomainService;
        }
        public void Handle(CreateFactorEvent @event)
        {
            _factorDomainService.FactorIsExist(@event.OrderId);
            var factorAddress = new FactorAddress(@event.OrderBase.OrderAddress.AddressText,
                @event.OrderBase.OrderAddress.PhoneNumber, @event.OrderBase.OrderAddress.CityId,
                @event.OrderBase.OrderAddress.CityName, @event.OrderBase.OrderAddress.Geography);
            var shopDiscountPrice = decimal.Round(@event.Price * (100 - @event.Discount) / 100);
            var factor = new Factor(@event.FactorId, @event.OrderId, @event.Price, 
                @event.OrderSuggestionId, @event.Discount, @event.Customer, 
                @event.Shop, factorAddress, shopDiscountPrice, 
                CalcRealPrice(@event.FactorRaws, @event.Discount),
                @event.ShippingTime)
            {
                FactorRaws = @event.FactorRaws
            };
            _repository.Add(factor);
            DomainEventDispatcher.Raise(new CreateHasFactorEvent(@event.OrderId, @event.Shop.Id, factor));
        }
        private decimal CalcRealPrice(List<FactorRaw> raws, int discountShop)
        {
            decimal totalPrice = 0;
            foreach (var factorRaw in raws)
            {
                if (factorRaw.Discount != null)
                {
                    if (factorRaw.Discount is FactorRawPercentDiscount factorRawPercentDiscount)
                    {
                        if (factorRaw.Discount.HasDiscountValid())
                        {
                            var price = factorRaw.Price * (100 - discountShop) / 100;
                            totalPrice += price * (decimal)(100 - factorRawPercentDiscount.Percent) / 100;

                            if (factorRaw.Quantity - 1 > 0)
                            {
                                var price1 = factorRaw.Price * (100 - discountShop) / 100;
                                totalPrice += price1 * (factorRaw.Quantity - 1);
                            }
                        }
                        else
                        {
                            var price = factorRaw.Price * (100 - discountShop) / 100;
                            totalPrice += price * factorRaw.Quantity;
                        }
                    }
                    else
                    {
                        var price = factorRaw.Price * (100 - discountShop) / 100;
                        totalPrice += price * factorRaw.Quantity;
                    }
                }
                else
                {
                    var price = factorRaw.Price * (100 - discountShop) / 100;
                    totalPrice += price * factorRaw.Quantity;
                }
            }
            return decimal.Round(totalPrice);
        }
    }
}
