using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.Orders;
using Shopping.QueryModel.Implements.Persons;
using Shopping.QueryModel.QueryModels.Orders;
using Shopping.QueryModel.QueryModels.Orders.Abstract;
using Shopping.QueryService.Implements.Factors;
using Shopping.QueryService.Interfaces.Orders;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Orders
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IReadOnlyRepository<Person, Guid> _personRepository;
        private readonly IReadOnlyRepository<OrderBase, long> _orderRepository;
        private readonly IReadOnlyRepository<Factor, long> _factorRepository;
        private readonly IReadOnlyRepository<ApplicationSetting, Guid> _settingRepository;
        private readonly IReadOnlyRepository<PrivateOrder, long> _privateOrderRepository;
        public OrderQueryService(
            IReadOnlyRepository<Person, Guid> personRepository,
            IReadOnlyRepository<OrderBase, long> orderRepository,
            IReadOnlyRepository<Factor, long> factorRepository,
            IReadOnlyRepository<PrivateOrder, long> privateOrderRepository,
            IReadOnlyRepository<ApplicationSetting, Guid> settingRepository)
        {
            _personRepository = personRepository;
            _orderRepository = orderRepository;
            _factorRepository = factorRepository;
            _privateOrderRepository = privateOrderRepository;
            _settingRepository = settingRepository;
        }
        public IQueryable<OrderBaseWithCustomerDto> GetOrders()
        {
            var result = _orderRepository.AsQuery().OfType<PrivateOrder>()
                .Select(item => new OrderBaseWithCustomerDto
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime,
                    OrderStatus = item.OrderStatus,
                    Description = item.Description,
                    ExpireTime = item.ExpireOpenTime,
                    Customer = new CustomerDto
                    {
                        PersonNumber = item.Customer.PersonNumber,
                        Id = item.Customer.Id,
                        IsActive = item.Customer.IsActive,
                        UserId = item.Customer.UserId,
                        EmailAddress = item.Customer.EmailAddress,
                        FirstName = item.Customer.FirstName,
                        LastName = item.Customer.LastName,
                        MobileNumber = item.Customer.MobileNumber,
                        CreationTime = item.CreationTime,

                    },
                    IsConvertToArea = item.IsConvertToAreaOrder
                });

            return result;
        }
        public IQueryable<OrderBaseDto> GetCustomerOrders(Guid customerId)
        {
            var result = _orderRepository.AsQuery().Where(item => item.Customer.Id == customerId)
                .Select(item => new OrderBaseWithCustomerDto
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime,
                    OrderStatus = item.OrderStatus,
                    Description = item.Description,
                    ExpireTime = item.ExpireOpenTime,
                    Customer = new CustomerDto
                    {
                        Id = item.Customer.Id,
                        IsActive = item.Customer.IsActive,
                        UserId = item.Customer.UserId,
                        EmailAddress = item.Customer.EmailAddress,
                        FirstName = item.Customer.FirstName,
                        LastName = item.Customer.LastName,
                        MobileNumber = item.Customer.MobileNumber,
                        CreationTime = item.CreationTime,
                        PersonNumber = item.Customer.PersonNumber
                    }
                });
            return result;
        }
        public IQueryable<OrderBaseDto> GetShopOrders(Guid shopId)
        {
            var result = _orderRepository.AsQuery().OfType<PrivateOrder>().Where(item => item.Shop.Id == shopId)
                .Select(item => new OrderBaseWithCustomerDto
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime,
                    OrderStatus = item.OrderStatus,
                    Description = item.Description,
                    ExpireTime = item.ExpireOpenTime,
                    Customer = new CustomerDto
                    {
                        Id = item.Customer.Id,
                        IsActive = item.Customer.IsActive,
                        UserId = item.Customer.UserId,
                        EmailAddress = item.Customer.EmailAddress,
                        FirstName = item.Customer.FirstName,
                        LastName = item.Customer.LastName,
                        MobileNumber = item.Customer.MobileNumber,
                        CreationTime = item.CreationTime,
                        PersonNumber = item.Customer.PersonNumber
                    }
                });
            return result;
        }
        public int GetPendingShopOrdersCount(Guid userId)
        {
            var shop = _personRepository.AsQuery().OfType<Shop>().SingleOrDefault(item => item.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }

            var privateOrdersCount = _orderRepository.AsQuery().OfType<PrivateOrder>().Count(item =>
                item.Shop.Id == shop.Id && !item.IsConvertToAreaOrder &&
                (item.OrderStatus == OrderStatus.Pending && (DateTime.Now <= item.ExpireOpenTime) ||
                 (item.OrderStatus == OrderStatus.SelfOpened && DateTime.Now <= item.ResponseExpireTime)));

            var areaOrdersCount = _orderRepository.AsQuery().OfType<AreaOrder>().Count(item =>
                item.Shop.Id == shop.Id &&
                (item.OrderStatus == OrderStatus.Pending && DateTime.Now <= item.ExpireOpenTime) ||
                (item.OrderStatus == OrderStatus.SelfOpened && DateTime.Now <= item.ResponseExpireTime));
            return privateOrdersCount + areaOrdersCount;
        }
        public async Task<MobilePagingResultDto<IOrderBaseFullInfoDto>> GetPendingShopOrders(Guid userId, PagedInputDto pagedInput)
        {
            var shop = await _personRepository.AsQuery().OfType<Shop>().SingleOrDefaultAsync(item => item.UserId == userId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var privateOrders = (IQueryable<OrderBase>)_orderRepository.AsQuery().OfType<PrivateOrder>().Where(item =>
               item.Shop.Id == shop.Id && !item.IsConvertToAreaOrder &&
               (item.OrderStatus == OrderStatus.Pending || item.OrderStatus == OrderStatus.SelfOpened));

            var areaOrders = (IQueryable<OrderBase>)_orderRepository.AsQuery().OfType<AreaOrder>().Where(item =>
               item.Shop.Id == shop.Id &&
                (item.OrderStatus == OrderStatus.Pending || item.OrderStatus == OrderStatus.SelfOpened ||
                    item.OrderStatus == OrderStatus.OtherOpened));

            var orders = privateOrders.Concat(areaOrders);

            var result = orders.OrderByDescending(p => p.CreationTime).Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList().Select(item => item.ToOrderBaseFullInfoDto()).ToList();
            return new MobilePagingResultDto<IOrderBaseFullInfoDto>
            {
                Count = result.Count,
                Result = result
            };
        }

        public MobilePagingResultDto<IOrderFactorDto> GetCustomerOrdersByUserId(Guid userId, PagedInputDto pagedInput)
        {
            var setting = _settingRepository.AsQuery().FirstOrDefault();
            var customer = _personRepository.AsQuery().OfType<Customer>().SingleOrDefault(p => p.UserId == userId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            var privateOrders = _orderRepository.AsQuery().OfType<PrivateOrder>()
                .Where(p => p.Customer.Id == customer.Id);
            var factors = _factorRepository.AsQuery().Where(p => p.Customer.Id == customer.Id);
            var orderFactors = privateOrders.GroupJoin(
                    factors,
                    o => o.Id,
                    f => f.OrderId,
                    (order, factor) => new { Order = order, Factors = factor })
                .SelectMany(item => item.Factors.DefaultIfEmpty(),
                    (order, factor) => new { order.Order, Factor = factor });

            var result = orderFactors.OrderByDescending(p => p.Order.CreationTime)
                .Skip(pagedInput.Skip)
                .Take(pagedInput.Count)
                .ToList()
                .Select(p => new OrderFactorDto
                {
                    Factor = p.Factor.ToFactorDto(),
                    Order = p.Order.ToOrderBaseFullInfoDto()
                })
                .ToList();
            foreach (var item in result)
            {
                if (item.Factor != null)
                {
                    var expireTime = DateTime.Now.AddMinutes(-setting.FactorExpireTime);
                    var factorExpireTimeDate = item.Factor.CreationTime.AddMinutes((double)setting.FactorExpireTime);
                    item.Factor.TimeLeft = (int)factorExpireTimeDate.Subtract(DateTime.Now).TotalMinutes;

                    item.Factor.IsExpired = item.Factor.CreationTime <= expireTime;
                }
            }
            return new MobilePagingResultDto<IOrderFactorDto>
            {
                Count = orderFactors.Count(),
                Result = result
            };
        }
        public IOrderBaseFullInfoDto Get(long id)
        {
            var order = _orderRepository.Find(id);
            var result = order.ToOrderBaseFullInfoDto();
            return result;
        }
        public async Task<CheckOrder> CheckOrderState(long orderId)
        {
            var privateOrder = await _orderRepository.AsQuery().OfType<PrivateOrder>()
                .SingleOrDefaultAsync(p => p.Id == orderId);
            if (privateOrder == null)
            {
                throw new DomainException("سفارش یافت نشد");
            }
            if (!privateOrder.IsConvertToAreaOrder)
            {
                switch (privateOrder.OrderStatus)
                {
                    case OrderStatus.Pending:
                        if (DateTime.Now <= privateOrder.ExpireOpenTime)
                        {
                            //در انتظار پاسخ فروشگاه
                            int totalExpireSecond = (int)privateOrder.ExpireOpenTime.Subtract(DateTime.Now).TotalSeconds;
                            return new CheckOrder("در انتظار پاسخ فروشگاه", totalExpireSecond, CheckOrderStatus.PendingSendForShop, false);
                        }
                        //عدم کارکرد زمانبند
                        return new CheckOrder("در انتظار پاسخ فروشگاه", 10, CheckOrderStatus.SchedulerDoesNotWork, false);
                    case OrderStatus.SelfOpened:
                        if (DateTime.Now <= privateOrder.ResponseExpireTime)
                        {
                            // باز شده توسط فروشگاه  و در حال پیشنهاد زدن
                            int totalExpireSecond = (int)privateOrder.ResponseExpireTime.Value.Subtract(DateTime.Now).TotalSeconds;
                            return new CheckOrder("باز شده توسط فروشگاه", totalExpireSecond, CheckOrderStatus.Opened, false);
                        }
                        //عدم ثبت پیشنهاد فروشگاه
                        return new CheckOrder("عدم ثبت پیشنهاد فروشگاه", 0, CheckOrderStatus.NotAccepted, true);
                    case OrderStatus.HasSuggestion:
                        //فروشگاه پیشنهاد ثبت کرده است
                        return new CheckOrder("فروشگاه پیشنهاد ثبت کرد", 0, CheckOrderStatus.ShopAddedSuggestion, true);
                }
            }
            else
            {
                var areaOrders = _orderRepository.AsQuery().OfType<AreaOrder>()
                    .Where(p => p.PrivateOrder.Id == privateOrder.Id);
                if (!areaOrders.Any())
                {
                    //فروشگاهی در محدوده نمی باشد برای ارسال
                    return new CheckOrder("فروشگاه دیگری در محدوده نمی باشد", 0, CheckOrderStatus.AnyShopsNotAround, true);
                }
                if (areaOrders.All(p => p.OrderStatus == OrderStatus.Pending))
                {
                    var areaOrder = areaOrders.OrderByDescending(p => p.CreationTime).FirstOrDefault();
                    // ReSharper disable once PossibleNullReferenceException
                    if (areaOrder.ExpireOpenTime >= DateTime.Now)
                    {
                        //در انتظار پاسخ دیگر فروشگاه ها
                        int totalExpireSecond = (int)areaOrder.ExpireOpenTime.Subtract(DateTime.Now).TotalSeconds;
                        return new CheckOrder("در انتظار پاسخ دیگر فروشگاه ها", totalExpireSecond, CheckOrderStatus.SendForOtherShops, false);
                    }
                    //زمان باز کردن توسط دیگر فروشگاه ها به پایان رسیده است
                    return new CheckOrder("زمان باز کردن توسط دیگر فروشگاه ها به پایان رسیده است", 0,
                        CheckOrderStatus.OtherNotOpened, true);
                }
                if (areaOrders.Any(p => p.OrderStatus == OrderStatus.SelfOpened))
                {
                    var openAreOrder = areaOrders.FirstOrDefault(p => p.OrderStatus == OrderStatus.SelfOpened);
                    // ReSharper disable once PossibleNullReferenceException
                    if (openAreOrder.ResponseExpireTime >= DateTime.Now)
                    {
                        // باز شده توسط یکی از فروشگاه ها و در حال پیشنهاد زدن
                        int totalExpireSecond = (int)openAreOrder.ResponseExpireTime.Value.Subtract(DateTime.Now).TotalSeconds;
                        return new CheckOrder("باز شده توسط یکی از فروشگاه ها و در حال پیشنهاد زدن", totalExpireSecond, CheckOrderStatus.OtherOpened, false);
                    }
                    return new CheckOrder("باز شده توسط یکی از فروشگاه ها و عدم ثبت پیشنهاد", 0, CheckOrderStatus.OtherNotAccepted, true);
                }
                if (areaOrders.Any(p => p.OrderStatus == OrderStatus.HasSuggestion))
                {
                    return new CheckOrder("یکی از فروشگاه ها پیشنهاد ثبت کرده است", 0, CheckOrderStatus.OtherAddedSuggestion, true);
                }
            }
            //todo check order change test
            return null;
        }
        public long CheckHasPendingOrder(Guid userId)
        {
            var customer = _personRepository.AsQuery().OfType<Customer>().SingleOrDefault(p => p.UserId == userId);

            if (customer == null)
            {
                throw new DomainException("کاربر یافت نشد");
            }
            var privateOrder = _privateOrderRepository.AsQuery().OrderByDescending(p => p.CreationTime)
                .FirstOrDefault(p =>
                    p.Customer.Id == customer.Id);

            if (privateOrder == null)
            {
                return 0;
            }

            if (privateOrder.OrderStatus == OrderStatus.Pending && privateOrder.ExpireOpenTime >= DateTime.Now)
            {
                return privateOrder.Id;
            }

            if (privateOrder.OrderStatus == OrderStatus.SelfOpened && privateOrder.ResponseExpireTime >= DateTime.Now)
            {
                return privateOrder.Id;
            }

            if (privateOrder.IsConvertToAreaOrder)
            {
                var areaOrders = _orderRepository.AsQuery().OfType<AreaOrder>()
                    .Where(p => p.PrivateOrder.Id == privateOrder.Id);
                if (areaOrders.All(p => p.OrderStatus == OrderStatus.Pending))
                {
                    var areaOrder = areaOrders.FirstOrDefault();

                    if (areaOrder != null && areaOrder.ExpireOpenTime >= DateTime.Now)
                    {
                        return areaOrder.PrivateOrder.Id;
                    }
                }
                if (areaOrders.Any(p => p.OrderStatus == OrderStatus.SelfOpened))
                {
                    var openAreOrder = areaOrders.FirstOrDefault(p => p.OrderStatus == OrderStatus.SelfOpened);
                    if (openAreOrder != null && openAreOrder.ResponseExpireTime >= DateTime.Now)
                    {
                        return openAreOrder.PrivateOrder.Id;
                    }
                }
            }
            return 0;
        }
        public IQueryable<AreaOrderWithShopDto> GetAreaOrderWithPrivateOrder(long privateOrderId)
        {
            var privateOrder = _orderRepository.AsQuery().OfType<PrivateOrder>()
                .SingleOrDefault(p => p.Id == privateOrderId);
            if (privateOrder == null)
            {
                throw new DomainException("سفارش یافت نشد");
            }
            if (!privateOrder.IsConvertToAreaOrder)
            {
                throw new DomainException("این سفارش تبدیل به سفارش منطقه ای نشده است");
            }
            var result = _orderRepository.AsQuery().OfType<AreaOrder>().Where(p => p.PrivateOrder.Id == privateOrder.Id)
                .Select(item => new AreaOrderWithShopDto
                {
                    Id = item.Id,
                    CreationTime = item.CreationTime,
                    OrderStatus = item.OrderStatus,
                    Description = item.Description,
                    ExpireTime = item.ExpireOpenTime,
                    Shop = new ShopDto
                    {
                        Id = item.Shop.Id,
                        UserId = item.Shop.UserId,
                        IsActive = item.Shop.IsActive,
                        Description = item.Shop.Description,
                        Name = item.Shop.Name,
                        EmailAddress = item.Shop.EmailAddress,
                        DefaultDiscount = item.Shop.DefaultDiscount,
                        DescriptionStatus = item.Shop.DescriptionStatus,
                        FirstName = item.Shop.FirstName,
                        LastName = item.Shop.LastName,
                        NationalCode = item.Shop.NationalCode,
                        ShopStatus = item.Shop.ShopStatus,
                        MobileNumber = item.Shop.MobileNumber,
                        CreationTime = item.Shop.CreationTime,
                        PersonNumber = item.Shop.PersonNumber,
                        HasMarketer = item.Shop.MarketerId == null,
                        MarketerId = item.Shop.MarketerId,
                        Metrage = item.Shop.Metrage,
                        AreaRadius = item.Shop.AreaRadius,

                    }
                });
            return result;
        }
    }
}