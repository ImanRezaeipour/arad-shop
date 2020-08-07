using AutoMapper;
using Shopping.DomainModel.Aggregates.FakeIos.Orders;
using Shopping.QueryModel.QueryModels.FakeIos.Orders;

namespace Shopping.QueryService.Implements.FakeIos
{
    public static class FakeIosMapper
    {
        public static IFakeOrderIosDto ToDto(this FakeOrderIos src)
        {
            return Mapper.Map<IFakeOrderIosDto>(src);
        }
    }
}