using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kernel.Notify.Message.Interfaces;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Orders.Commands.PrivateOrders;
using Shopping.Commands.Commands.Orders.Responses;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Interfaces;
using Shopping.DomainModel.Aggregates.Discounts.Interfaces;
using Shopping.DomainModel.Aggregates.Factors.Interfaces;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.Interfaces;
using Shopping.DomainModel.Aggregates.Orders.ValueObjects;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Products.Aggregates;
using Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount;
using Shopping.DomainModel.Aggregates.ShopOrderLogs.Events;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;

namespace Shopping.CommandHandler.CommandHandlers.Order
{
    public class PrivateOrderCommandHandler
    : ICommandHandler<CreatePrivateOrderCommand, CreatePrivateOrderCommandResponse>
    , ICommandHandler<OpenPrivateOrderCommand, OpenPrivateOrderCommandResponse>
        , ICommandHandler<ConvertPrivateToAreaOrderCommand, ConvertPrivateToAreaOrderCommandResponse>
        , ICommandHandler<CancelPrivateOrderConmmand, CancelPrivateOrderConmmandResponse>
    {
        private readonly IContext _context;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<ApplicationSetting> _applicationSettingRepository;
        private readonly IFcmNotification _fcmNotification;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IFactorDomainService _factorDomainService;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<PrivateOrder> _privateOrderRepository;
        private readonly IRepository<AreaOrder> _areaRepository;
        private readonly IRepository<OrderBase> _orderRepository;

        private readonly IPercentDiscountDomainService _percentDiscountDomainService;
        private readonly IApplicationSettingDomainService _applicationSettingDomainService;
        public PrivateOrderCommandHandler(IRepository<Person> personRepository,
            IFcmNotification fcmNotification,
            IRepository<ApplicationSetting> applicationSettingRepository,
            IOrderDomainService orderDomainService,
            IFactorDomainService factorDomainService,
            IRepository<Product> productRepository,
            IPercentDiscountDomainService percentDiscountDomainService,
            IApplicationSettingDomainService applicationSettingDomainService,
            IRepository<PrivateOrder> privateOrderRepository,
            IContext context, IRepository<AreaOrder> areaRepository,
            IRepository<OrderBase> orderRepository)
        {
            _personRepository = personRepository;
            _fcmNotification = fcmNotification;
            _applicationSettingRepository = applicationSettingRepository;
            _orderDomainService = orderDomainService;
            _factorDomainService = factorDomainService;
            _productRepository = productRepository;
            _percentDiscountDomainService = percentDiscountDomainService;
            _applicationSettingDomainService = applicationSettingDomainService;
            _privateOrderRepository = privateOrderRepository;
            _context = context;
            _areaRepository = areaRepository;
            _orderRepository = orderRepository;
        }
        public async Task<CreatePrivateOrderCommandResponse> Handle(CreatePrivateOrderCommand command)
        {
            var appSetting = _applicationSettingRepository.AsQuery().FirstOrDefault();
            if (appSetting == null)
            {
                throw new Exception();
            }
            var customer = _personRepository.AsQuery().OfType<Customer>().SingleOrDefault(p => p.UserId == command.UserId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            await _orderDomainService.CheckCustomerRequestOrderDuration(customer, appSetting);
            var customerAddress =
                customer.CustomerAddresses.SingleOrDefault(p => p.Id == command.CustomerAddressId);
            if (customerAddress == null)
            {
                throw new DomainException("ادرس مشتری یافت نشد");
            }
            var shop = _personRepository.AsQuery().OfType<Shop>().SingleOrDefault(p => p.Id == command.ShopId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه وارد شده یافت نشد");
            }
            var orderAddress = new OrderAddress(customerAddress.AddressText, customerAddress.PhoneNumber,
                customerAddress.CityId, customerAddress.CityName, customerAddress.Geography);
            var privateOrder = new PrivateOrder(customer, orderAddress, command.Description, DateTime.Now.AddSeconds(appSetting.OrderExpireOpenTime), shop)
            {
                OrderItems = new List<OrderItem>()
            };
            if (command.OrderItems.Any(p => p.IsPercentDiscount))
            {
                if (_factorDomainService.HavePercentDiscountToday(customer))
                {
                    throw new DomainException("شما مجاز به انتخاب یک کالای دارای تخفیف درصدی در هر روز می باشید");
                }
            }
            decimal totalPrice = 0;
            foreach (var orderItem in command.OrderItems)
            {
                var product = _productRepository.Find(orderItem.ProductId);
                if (product == null)
                {
                    throw new DomainException("کالای انتخابی یافت نشد");
                }
                var orderProduct = new OrderProduct(product.Id, product.Name, product.Price, product.MainImage, product.Brand.Id, product.Brand.Name);
                OrderItemDiscountBase orderItemDiscount = null;
                if (orderItem.IsPercentDiscount)
                {
                    if (product.ProductDiscount != null)
                    {
                        if (product.ProductDiscount is ProductPercentDiscount productPercentDiscount)
                        {
                            if (productPercentDiscount.HasDiscountValid())
                            {
                                var haveRemainOrderCount = _percentDiscountDomainService.HaveRemainOrderCount(productPercentDiscount.DiscountId,customer);
                                if (haveRemainOrderCount)
                                {
                                    orderItemDiscount = new OrderItemPercentDiscount(Guid.NewGuid(),
                                        productPercentDiscount.DiscountId, productPercentDiscount.Title, productPercentDiscount.FromDate,
                                        productPercentDiscount.ToDate, productPercentDiscount.Percent, productPercentDiscount.FromTime,
                                        productPercentDiscount.ToTime);
                                    _percentDiscountDomainService.LowOfNumberRemainOrderCount(productPercentDiscount.DiscountId, customer);

                                }
                            }
                        }
                    }
                }
                privateOrder.OrderItems.Add(new OrderItem(Guid.NewGuid(), orderItem.Quantity,
                    orderItem.Description, orderProduct, orderItemDiscount));
                totalPrice = totalPrice + (product.Price * orderItem.Quantity);
            }
            if (privateOrder.OrderItems.Count(item => item.Discount != null) > 1)
            {
                throw new DomainException("شما مجاز به انتخاب یک کالای دارای تخفیف درصدی در هر روز می باشید");
            }
            _applicationSettingDomainService.CheckMinimumBuy(totalPrice);
            _orderDomainService.CalcOrderPercentDiscount(privateOrder);
            _privateOrderRepository.Add(privateOrder);
            DomainEventDispatcher.Raise(new CreateShopOrderLogEvent(shop, privateOrder));
            _context.SaveChanges();
            await _fcmNotification.SendToIds(shop.GetPushTokens(), "سفارش جدید",
                $"یک سفارش ثبت شد", NotificationType.OrderAdd,
                AppType.Shop, NotificationSound.Shopper);
            SendNotificationToBoss(privateOrder, shop, customer);
            return new CreatePrivateOrderCommandResponse(privateOrder.Id, appSetting.OrderExpireOpenTime);
        }
        private void SendNotificationToBoss(OrderBase order, Shop shop, Customer customer)
        {
            //var bossMobileNumber = ConfigurationManager.AppSettings["BossMobileNumber"];
            //var boss = _personRepository.AsQuery().OfType<Shop>()
            //    .FirstOrDefault(item => item.MobileNumber == bossMobileNumber);
            //if (boss != null)
            //{
            //    _fcmNotification.SendToIds(boss.GetPushTokens(), $"سفارش {order.Id}-{customer.FirstName} {customer.LastName}-{customer.MobileNumber}",
            //        $"شماره سفارش: {order.Id}\n" +
            //        $"فروشگاه: {shop.Name}-{shop.FirstName} {shop.LastName}-{shop.ShopAddress.PhoneNumber}-{shop.MobileNumber} \n" +
            //        $"مشتری: {customer.FirstName} {customer.LastName}-{customer.MobileNumber} \n",
            //        NotificationType.OrderAdd, AppType.Shop, NotificationSound.Shopper);
            //}
        }
        public async Task<OpenPrivateOrderCommandResponse> Handle(OpenPrivateOrderCommand command)
        {
            var setting = _applicationSettingRepository.AsQuery().FirstOrDefault();
            var order = await _orderRepository.FindAsync(command.OrderId);
            if (order == null)
            {
                throw new DomainException("سفارش یافت نشد");
            }
            if (order.OrderStatus == OrderStatus.SelfOpened)
            {
                return new OpenPrivateOrderCommandResponse();
            }

            if (order is PrivateOrder privateOrder)
            {
                privateOrder.Opened(setting.OrderItemResponseTime);
            }

            else if (order is AreaOrder areaOrder)
            {
                if (_areaRepository.AsQuery()
                    .Any(p => p.PrivateOrder.Id == areaOrder.PrivateOrder.Id && p.OrderStatus == OrderStatus.SelfOpened && p.Id != areaOrder.Id))
                {
                    throw new DomainException("این سفارش توسط یک فروشگاه دیگر باز شده است");
                }
                if (areaOrder.OrderStatus != OrderStatus.Pending)
                {
                    throw new DomainException("وضعیت جاری سفارش نامعتبر می باشد");
                }
                if (areaOrder.ExpireOpenTime <= DateTime.Now)
                {
                    throw new DomainException("مدت زمان بازکردن سفارش توسط شما به پایان رسیده است");
                }
                areaOrder.OrderStatus = OrderStatus.SelfOpened;
                areaOrder.ResponseExpireTime =
                    DateTime.Now.AddSeconds(areaOrder.OrderItems.Count * setting.OrderItemResponseTime);
                var otherAreaOrderShop = _areaRepository.AsQuery()
                    .Where(p => p.PrivateOrder.Id == areaOrder.PrivateOrder.Id && p.Id != areaOrder.Id).ToList();
                foreach (var item in otherAreaOrderShop)
                {
                    item.OrderStatus = OrderStatus.OtherOpened;
                }
            }
            return new OpenPrivateOrderCommandResponse();
        }

        public async Task<ConvertPrivateToAreaOrderCommandResponse> Handle(ConvertPrivateToAreaOrderCommand command)
        {
            var setting = await _applicationSettingRepository.AsQuery().FirstOrDefaultAsync();
            if (setting == null)
            {
                throw new Exception();
            }
            var order = await _privateOrderRepository.FindAsync(command.OrderId);
            if (order == null)
            {
                throw new DomainException("سفارش یافت نشد");
            }

            var areaOrders = _areaRepository.AsQuery().Where(p => p.PrivateOrder.Id == order.Id).ToList();
            foreach (var areaOrder in areaOrders)
            {
                areaOrder.OrderItems.Clear();
                _areaRepository.Remove(areaOrder);
            }

            var shopsInArea = _personRepository.AsQuery().OfType<Shop>().Where(item =>
                    item.Id != order.Shop.Id && item.IsActive && item.ShopStatus == ShopStatus.Accept &&
                    item.ShopAddress.Geography.Distance(order.OrderAddress.Geography) <= item.AreaRadius)
                .ToList();
            if (shopsInArea.Count <= 1)
            {
                throw new DomainException("فروشگاه دیگری در اطراف این فرو شگاه موجود نمی باشد");
            }
            var orderItems = order.OrderItems.ToList();
            var areaOrderExpireOpenTime = DateTime.Now.AddSeconds(setting.OrderExpireOpenTime);
            foreach (var shop in shopsInArea)
            {
                var areaOrder = new AreaOrder(order.Customer, order.OrderAddress,
                    order.Description,
                    areaOrderExpireOpenTime, shop, order, AreaOrderCreator.ByCustomer)
                {
                    OrderItems = new List<OrderItem>()
                };
                foreach (var orderItem in orderItems)
                {
                    var orderProductTemp = new OrderProduct(orderItem.OrderProduct.ProductId,
                        orderItem.OrderProduct.Name, orderItem.OrderProduct.Price,
                        orderItem.OrderProduct.ProductImage, orderItem.OrderProduct.BrandId,
                        orderItem.OrderProduct.BrandName);
                    OrderItemDiscountBase orderItemDiscountBase = null;
                    if (orderItem.Discount != null)
                    {
                        orderItemDiscountBase = orderItem.Discount;
                    }
                    var orderItemTemp = new OrderItem(Guid.NewGuid(), orderItem.Quantity, orderItem.Description,
                        orderProductTemp, orderItemDiscountBase);
                    areaOrder.OrderItems.Add(orderItemTemp);
                }
                _areaRepository.Add(areaOrder);
                await _fcmNotification.SendToIds(shop.GetPushTokens(), "سفارش جدید",
                    $"یک سفارش ثبت شد", NotificationType.OrderAdd,
                    AppType.Shop, NotificationSound.Shopper);
            }
            order.IsConvertToAreaOrder = true;
            order.OrderStatus = OrderStatus.Pending;
            order.ExpireOpenTime = DateTime.Now.AddSeconds(setting.OrderExpireOpenTime);
            return new ConvertPrivateToAreaOrderCommandResponse(setting.OrderExpireOpenTime);
        }
        public async Task<CancelPrivateOrderConmmandResponse> Handle(CancelPrivateOrderConmmand command)
        {
            var privateOrder = await _privateOrderRepository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.OrderId);
            if (privateOrder == null)
            {
                throw new DomainException("سفارش یافت نشد");
            }
            privateOrder.OrderStatus = OrderStatus.Cancel;
            return new CancelPrivateOrderConmmandResponse();
        }
    }
}