using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kernel.Notify.Message.Interfaces;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.OrdersSuggestions.Commands;
using Shopping.Commands.Commands.OrdersSuggestions.Responses;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Interfaces;
using Shopping.DomainModel.Aggregates.Factors.Entities;
using Shopping.DomainModel.Aggregates.Factors.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Factors.Events;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.Events;
using Shopping.DomainModel.Aggregates.Orders.Interfaces;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.ShopOrderLogs.Events;
using Shopping.Infrastructure;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;

namespace Shopping.CommandHandler.CommandHandlers.OrdersSuggestions
{
    public class OrderSuggestionCommandHandler
        : ICommandHandler<CreateOrderSuggestionCommand, CreateOrderSuggestionCommandResponse>
        , ICommandHandler<AcceptOrderSuggestionCommand, AcceptOrderSuggestionCommandResponse>
        , ICommandHandler<RejectOrderSuggestionCommand, RejectOrderSuggestionCommandResponse>
    {
        private readonly ISeqRepository _seqRepository;
        private readonly IRepository<OrderBase> _orderRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<OrderSuggestion> _repository;
        private readonly IFcmNotification _fcmNotification;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IRepository<ApplicationSetting> _applicationSettingRepository;
        private readonly IApplicationSettingDomainService _applicationSettingDomainService;
        public OrderSuggestionCommandHandler(IRepository<OrderBase> orderRepository,
            IRepository<Person> personRepository,
            IRepository<Product> productRepository,
            IRepository<OrderSuggestion> repository,
            IFcmNotification fcmNotification,
            IRepository<ApplicationSetting> applicationSettingRepository,
            ISeqRepository seqRepository,
            IApplicationSettingDomainService applicationSettingDomainService,
            IOrderDomainService orderDomainService)
        {
            _personRepository = personRepository;
            _productRepository = productRepository;
            _repository = repository;
            _fcmNotification = fcmNotification;
            _applicationSettingRepository = applicationSettingRepository;
            _seqRepository = seqRepository;
            _applicationSettingDomainService = applicationSettingDomainService;
            _orderDomainService = orderDomainService;
            _orderRepository = orderRepository;
        }
        public async Task<CreateOrderSuggestionCommandResponse> Handle(CreateOrderSuggestionCommand command)
        {
            _applicationSettingDomainService.CheckDiscountInRangeSetting(command.Discount);
            _applicationSettingDomainService.CheckShippingTime(command.ShippingTime);
            if (_repository.AsQuery().Any(p => p.OrderId == command.OrderId))
            {
                throw new DomainException("پیشنهاد بروی این سفارش زده شده است");
            }
            var order = _orderRepository.AsQuery().SingleOrDefault(p => p.Id == command.OrderId);
            if (order == null)
            {
                throw new DomainException("سفارش یافت نشد");
            }

            if (order.OrderStatus == OrderStatus.Cancel)
            {
                throw new DomainException("این سفارش توسط مشتری لغو شده است");
            }
            _applicationSettingDomainService.CheckOrderExpireTime(order.CreationTime);
            if (order.ResponseExpireTime < DateTime.Now)
            {
                throw new DomainException("زمان پاسخ گویی شما به پایان رسیده است");
            }

            var shop = _personRepository.AsQuery().OfType<Shop>().SingleOrDefault(p => p.UserId == command.UserId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }

            var appSetting = _applicationSettingRepository.AsQuery().SingleOrDefault();
            if (appSetting == null)
            {
                throw new Exception();
            }

            if (command.Discount < appSetting.MinimumDiscount || command.Discount > appSetting.MaximumDiscount)
            {
                throw new DomainException("تخفیف وارد شده در بازه تخفیفات معتبر نمی باشد");
            }
            if (command.ShippingTime > appSetting.MaximumDeliveryTime)
            {
                throw new DomainException("زمان ارسال سفارش از حداکثر زمان ارسال معتبر بیشتر می باشد");
            }

            var orderId = order is AreaOrder areaOrder ? areaOrder.PrivateOrder.Id : order.Id;
            var orderSuggestion = new OrderSuggestion(Guid.NewGuid(), orderId, command.Discount, command.Description, shop, command.ShippingTime)
            {
                OrderSuggestionItems = new List<OrderSuggestionItemBase>()
            };
            decimal totalPrice = 0;
            foreach (var item in command.OrderSuggestionItems)
            {
                var orderItem = order.OrderItems.SingleOrDefault(p => p.Id == item.OrderItemId);
                if (orderItem == null)
                {
                    throw new DomainException("آیتم سفارش یافت نشد");
                }
                var findProduct = _productRepository.Find(item.ProductId);
                switch (item.OrderSuggestionItemType)
                {
                    case OrderSuggestionItemType.AlternativeProduct:
                        if (findProduct == null)
                        {
                            throw new DomainException("کالای ارسالی جایگزین نامعتبر می باشد");
                        }
                        var alternativeProduct =
                            new AlternativeProductSuggestion(findProduct.Id, findProduct.Name, findProduct.MainImage,
                                findProduct.Brand.Id, findProduct.Brand.Name);
                        orderItem.Discount = null;
                        var alternativeProductSuggestionItem = new AlternativeProductSuggestionItem(Guid.NewGuid(),
                            orderItem, item.Quantity, item.Description, item.Price, alternativeProduct,
                            orderItem.Quantity != item.Quantity);
                        orderSuggestion.OrderSuggestionItems.Add(alternativeProductSuggestionItem);
                        totalPrice = totalPrice + (item.Price * item.Quantity);
                        break;
                    case OrderSuggestionItemType.NoProduct:
                        var noProduct = new NoProductSuggestionItem(Guid.NewGuid(), orderItem);
                        orderSuggestion.OrderSuggestionItems.Add(noProduct);
                        break;
                    case OrderSuggestionItemType.HasProduct:
                        if (orderItem.Discount != null)
                        {
                            if (!orderItem.Discount.HasDiscountValid())
                            {
                                orderItem.Discount = null;
                            }
                        }
                        var hasProduct = new HasProductSuggestionItem(Guid.NewGuid(), orderItem, item.Quantity,
                            item.Description, item.Price, orderItem.Quantity != item.Quantity);
                        orderSuggestion.OrderSuggestionItems.Add(hasProduct);
                        totalPrice = totalPrice + (item.Price * item.Quantity);
                        break;
                }
            }
            _repository.Add(orderSuggestion);


            //todo order check it
            var expireMinutes = order.ExpireOpenTime.Subtract(DateTime.Now).TotalMinutes;
            order.ExpireOpenTime = order.ExpireOpenTime.AddMinutes(appSetting.OrderSuggestionExpireTime - expireMinutes);
            order.SuggestionTime = DateTime.Now.AddMinutes(appSetting.OrderSuggestionExpireTime);
            DomainEventDispatcher.Raise(new TheOrderStatusWentToHasSuggestionEvent(order));
            if (order is AreaOrder area)
            {
                area.PrivateOrder.SuggestionTime = DateTime.Now.AddMinutes(appSetting.OrderSuggestionExpireTime);
                DomainEventDispatcher.Raise(new TheOrderStatusWentToHasSuggestionEvent(area.PrivateOrder));
            }
            DomainEventDispatcher.Raise(new CreateHasSuggestionsEvent(order.Id, shop.Id, orderSuggestion));
            await _fcmNotification.SendToIds(order.Customer.GetPushTokens(),
                 "پیشنهاد سفارش",
                  $"برای سفارش شما با شناسه {order.Id} یک پیشنهاد ثبت شد",
                  NotificationType.OrderSuggestionAdded,
                  AppType.Customer, NotificationSound.Shopper);
            return new CreateOrderSuggestionCommandResponse();
        }
        public async Task<AcceptOrderSuggestionCommandResponse> Handle(AcceptOrderSuggestionCommand command)
        {
            var setting = _applicationSettingRepository.AsQuery().FirstOrDefault();

            var orderSuggestion = await _repository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.OrderSuggestionId);
            if (orderSuggestion == null)
            {
                throw new DomainException("پیشنهاد سفارش یافت نشد");
            }
            var order = _orderRepository.AsQuery().SingleOrDefault(p => p.Id == orderSuggestion.OrderId);
            if (order == null)
            {
                throw new DomainException("سفارش یافت نشد");
            }

            if (DateTime.Now > orderSuggestion.CreationTime.AddMinutes(setting.OrderSuggestionExpireTime))
            {
                throw new DomainException("زمان تایید پیشنهاد پایان یافته است");
            }
            var selectedOrderSuggestionItems =
                orderSuggestion.OrderSuggestionItems.Where(p => command.AcceptOrderSuggestionItem.Any(i => i.OrderSuggestionItemId == p.Id))
                    .ToList();
            long factorId = _seqRepository.GetNextSequenceValue(SqNames.FactorIdSequence);

            List<FactorRaw> factorRaws = new List<FactorRaw>();

            var havePercentDiscountToday = _orderDomainService.CheckOrderPercentDiscountToday(order.Customer, orderSuggestion.OrderId);
            foreach (var selectedOrderSuggestionItem in selectedOrderSuggestionItems)
            {
                switch (selectedOrderSuggestionItem)
                {
                    case AlternativeProductSuggestionItem alternativeProductSuggestionItem:
                        {
                            var itemQuantity = command.AcceptOrderSuggestionItem.SingleOrDefault(p =>
                                p.OrderSuggestionItemId == selectedOrderSuggestionItem.Id);
                            var quantity = itemQuantity?.Quantity ?? alternativeProductSuggestionItem.Quantity;
                            var product = _productRepository.Find(alternativeProductSuggestionItem
                                .AlternativeProductSuggestion.ProductId);
                            if (product == null)
                            {
                                throw new DomainException("محصول جایگزین یافت نشد");
                            }
                            var factorRaw = new FactorRaw(product.Id, quantity,
                                   alternativeProductSuggestionItem.Description,
                                   alternativeProductSuggestionItem.Price, product.Name,
                                   alternativeProductSuggestionItem.AlternativeProductSuggestion
                                       .ProductImage, product.Brand.Id, product.Brand.Name, null);
                            factorRaws.Add(factorRaw);
                            break;
                        }
                    case HasProductSuggestionItem hasProductSuggestionItem:
                        {
                            var itemQuantity = command.AcceptOrderSuggestionItem.SingleOrDefault(p =>
                                p.OrderSuggestionItemId == selectedOrderSuggestionItem.Id);
                            var quantity = itemQuantity?.Quantity ?? hasProductSuggestionItem.Quantity;
                            var product = _productRepository.Find(hasProductSuggestionItem.OrderItem.OrderProduct.ProductId);
                            FactorRaw factorRaw;
                            if (havePercentDiscountToday)
                            {
                                if (hasProductSuggestionItem.OrderItem.Discount != null)
                                {
                                    if (hasProductSuggestionItem.OrderItem.Discount is OrderItemPercentDiscount
                                        orderItemPercentDiscount)
                                    {
                                        if (orderItemPercentDiscount.HasDiscountValid())
                                        {
                                            factorRaw = new FactorRaw(product.Id, quantity,
                                                hasProductSuggestionItem.Description,
                                                hasProductSuggestionItem.Price, product.Name,
                                                hasProductSuggestionItem.OrderItem.OrderProduct.ProductImage,
                                                product.Brand.Id, product.Brand.Name,
                                                new FactorRawPercentDiscount(Guid.NewGuid(),
                                                    orderItemPercentDiscount.DiscountId,
                                                    orderItemPercentDiscount.DiscountTitle,
                                                    orderItemPercentDiscount.FromDate, orderItemPercentDiscount.ToDate,
                                                    orderItemPercentDiscount.Percent, orderItemPercentDiscount.FromTime,
                                                    orderItemPercentDiscount.ToTime));
                                        }
                                        else
                                        {
                                            factorRaw = new FactorRaw(product.Id, quantity,
                                                hasProductSuggestionItem.Description,
                                                hasProductSuggestionItem.Price, product.Name,
                                                hasProductSuggestionItem.OrderItem.OrderProduct.ProductImage,
                                                product.Brand.Id,
                                                product.Brand.Name, null);
                                        }
                                    }
                                    else
                                    {
                                        factorRaw = new FactorRaw(product.Id, quantity,
                                            hasProductSuggestionItem.Description,
                                            hasProductSuggestionItem.Price, product.Name,
                                            hasProductSuggestionItem.OrderItem.OrderProduct.ProductImage,
                                            product.Brand.Id,
                                            product.Brand.Name, null);
                                    }
                                }
                                else
                                {
                                    factorRaw = new FactorRaw(product.Id, quantity,
                                        hasProductSuggestionItem.Description,
                                        hasProductSuggestionItem.Price, product.Name,
                                        hasProductSuggestionItem.OrderItem.OrderProduct.ProductImage, product.Brand.Id,
                                        product.Brand.Name, null);
                                }
                            }
                            else
                            {
                                factorRaw = new FactorRaw(product.Id, quantity,
                                    hasProductSuggestionItem.Description,
                                    hasProductSuggestionItem.Price, product.Name,
                                    hasProductSuggestionItem.OrderItem.OrderProduct.ProductImage, product.Brand.Id,
                                    product.Brand.Name, null);
                            }
                            factorRaws.Add(factorRaw);
                            break;
                        }
                }
            }
            DomainEventDispatcher.Raise(new TheOrderStatusWentToAcceptEvent(order));
            DomainEventDispatcher.Raise(new CreateFactorEvent(factorId, order.Id, orderSuggestion.Id,
                CalcTotalPrice(factorRaws), orderSuggestion.Discount,
                factorRaws, orderSuggestion.Shop, order.Customer, order, orderSuggestion.ShippingTime));
            orderSuggestion.OrderSuggestionStatus = OrderSuggestionStatus.Accept;
            return new AcceptOrderSuggestionCommandResponse(factorId);
        }
        public Task<RejectOrderSuggestionCommandResponse> Handle(RejectOrderSuggestionCommand command)
        {
            var orderSuggestion = _repository.AsQuery().SingleOrDefault(p => p.Id == command.OrderSuggestionId);
            if (orderSuggestion == null)
            {
                throw new DomainException("پیشنهادسفارش یافت نشد");
            }
            orderSuggestion.OrderSuggestionStatus = OrderSuggestionStatus.Reject;
            return Task.FromResult(new RejectOrderSuggestionCommandResponse());
        }
        private decimal CalcTotalPrice(List<FactorRaw> factorRaws)
        {
            decimal totalPrice = 0;
            foreach (var factorRawEventItem in factorRaws)
            {
                totalPrice += factorRawEventItem.Price * factorRawEventItem.Quantity;
            }
            return totalPrice;
        }
    }
}