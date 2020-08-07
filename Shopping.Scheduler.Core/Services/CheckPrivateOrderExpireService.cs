using System;
using System.Collections.Generic;
using System.Linq;
using Kernel.Notify.Message.Implements;
using Kernel.Notify.Message.Interfaces;
using Newtonsoft.Json;
using NLog;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.ValueObjects;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Scheduler.Core.Contexts;
using Shopping.Scheduler.Core.Interfaces;
using Shopping.Shared.Enums;

namespace Shopping.Scheduler.Core.Services
{
    public class CheckPrivateOrderExpireService : ICheckPrivateOrderExpireService
    {
        private readonly Logger _logger;
        private readonly IFcmNotification _fcmNotification;
        public CheckPrivateOrderExpireService()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _fcmNotification = new FcmNotification();

        }

        public void CreateAreaOrder()
        {
            try
            {
                _logger.Info($"run at{DateTime.Now}");
                using (var db = new ShoppingSchedulerContext())
                {
                    var setting = db.Settings.First();
                    if (setting == null) return;
                    var now = DateTime.Now;
                    var date = DateTime.Parse("2019/05/19");
                    var ordersExpire = db.PrivateOrder.Where(p => p.CreationTime >= date &&
                        p.OrderStatus == OrderStatus.Pending && p.ExpireOpenTime < now &&
                        p.IsConvertToAreaOrder == false).ToList();
                  
                    foreach (var order in ordersExpire)
                    {
                        var shopsInArea = db.Shops.Where(item =>
                                item.IsActive && item.ShopStatus == ShopStatus.Accept && item.Id != order.Shop.Id &&
                                item.ShopAddress.Geography.Distance(order.OrderAddress.Geography) <= item.AreaRadius)
                            .ToList();
                        if (shopsInArea.Count < 1)
                        {
                            order.IsConvertToAreaOrder = true;
                            db.SaveChanges();
                            continue;
                        }

                        var expireOpenTime = DateTime.Now.AddSeconds(setting.OrderExpireOpenTime);
                        var orderItems = order.OrderItems.ToList();

                        foreach (var shop in shopsInArea)
                        {
                            var areaOrder = new AreaOrder(order.Customer, order.OrderAddress,
                                order.Description,
                                expireOpenTime, shop, order, AreaOrderCreator.ByScheduler)
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

                                var orderItemTemp = new OrderItem(Guid.NewGuid(), orderItem.Quantity,
                                    orderItem.Description,
                                    orderProductTemp, orderItemDiscountBase);

                                areaOrder.OrderItems.Add(orderItemTemp);
                            }
                            db.AreaOrder.Add(areaOrder);
                            _fcmNotification.SendToIds(shop.GetPushTokens(), "سفارش جدید",
                                $"یک سفارش ثبت شد", NotificationType.OrderAdd,
                                AppType.Shop, NotificationSound.Shopper);
                        }
                        order.IsConvertToAreaOrder = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Info($"exception run at{DateTime.Now}");
                _logger.Error(JsonConvert.SerializeObject(e));
            }
        }
    }
}