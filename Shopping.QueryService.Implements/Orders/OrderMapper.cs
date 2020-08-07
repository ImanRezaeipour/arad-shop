using AutoMapper;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.QueryModel.QueryModels.Orders.Abstract;

namespace Shopping.QueryService.Implements.Orders
{
    public static class OrderMapper
    {
        public static IOrderBaseDto ToOrderBaseDto(this OrderBase src)
        {
            return Mapper.Map<IOrderBaseDto>(src);
        }
        public static IOrderBaseFullInfoDto ToOrderBaseFullInfoDto(this OrderBase src)
        {
            return Mapper.Map<IOrderBaseFullInfoDto>(src);
        }
    }
}