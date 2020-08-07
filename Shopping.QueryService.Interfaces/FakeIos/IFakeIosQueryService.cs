using System.Collections.Generic;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.FakeIos.Orders;

namespace Shopping.QueryService.Interfaces.FakeIos
{
    public interface IFakeIosQueryService
    {
        IList<IFakeOrderIosDto> GetOrders(FakeOrderIosState? state);
        int GetOrdersCount(FakeOrderIosState? state);
    }
}   