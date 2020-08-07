using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.Implements.Orders;
using Shopping.QueryModel.QueryModels.Orders;
using Shopping.QueryModel.QueryModels.Orders.Abstract;

namespace Shopping.QueryService.Interfaces.Orders
{
    public interface IOrderQueryService
    {
        Task<MobilePagingResultDto<IOrderBaseFullInfoDto>> GetPendingShopOrders(Guid userId, PagedInputDto pagedInput);
        int GetPendingShopOrdersCount(Guid userId);
        MobilePagingResultDto<IOrderFactorDto> GetCustomerOrdersByUserId(Guid userId, PagedInputDto pagedInput);
        IQueryable<OrderBaseWithCustomerDto> GetOrders();
        IQueryable<OrderBaseDto> GetCustomerOrders(Guid customerId);
        IQueryable<OrderBaseDto> GetShopOrders(Guid shopId);
        IOrderBaseFullInfoDto Get(long id);
        Task<CheckOrder> CheckOrderState(long orderId);
        long CheckHasPendingOrder(Guid userId);
        IQueryable<AreaOrderWithShopDto> GetAreaOrderWithPrivateOrder(long privateOrderId);
    }
}