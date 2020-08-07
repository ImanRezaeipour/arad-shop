using System;
using System.Linq;
using AutoMapper;
using Shopping.DomainModel.Aggregates.Orders.Aggregates;
using Shopping.DomainModel.Aggregates.Orders.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Orders.Entities;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;
using Shopping.DomainModel.Aggregates.Orders.ValueObjects;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.Helper;
using Shopping.QueryModel.Implements;
using Shopping.QueryModel.Implements.Orders;
using Shopping.QueryModel.QueryModels.Orders;
using Shopping.QueryModel.QueryModels.Orders.Abstract;
using Shopping.QueryModel.QueryModels.Orders.Discounts;
using Shopping.QueryModel.QueryModels.Orders.Items;


namespace Shopping.QueryService.Implements.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderBase, IOrderBaseDto>()
                .ForMember(dest => dest.OrderType, opt => opt.ResolveUsing(GetOrderType))
                .ForMember(dest => dest.IsExpired, opt => opt.ResolveUsing(CheckIsExpired))
                .ForMember(dest => dest.TimeLeft, opt => opt.ResolveUsing(CalcTimeLeft));


            CreateMap<OrderBase, OrderBaseWithCustomerDto>()
                .ForMember(dest => dest.TimeLeft, opt => opt.ResolveUsing(CalcTimeLeft));

            CreateMap<OrderBase, IOrderBaseFullInfoDto>()
                .Include<PrivateOrder, IPrivateOrderDto>()
                .ForMember(dest => dest.OrderType, opt => opt.ResolveUsing(GetOrderType))
                .ForMember(dest => dest.IsExpired, opt => opt.ResolveUsing(CheckIsExpired))
                .ForMember(dest => dest.ItemsCount, opt => opt.MapFrom(src => src.OrderItems.Count))
                .ForMember(dest => dest.TotalPrice, opt => opt.ResolveUsing(CalcTotalPrice))
                .ForMember(dest => dest.TimeLeft, opt => opt.ResolveUsing(CalcTimeLeft));

            CreateMap<PrivateOrder, IPrivateOrderDto>();

            CreateMap<OrderAddress, IOrderAddressDto>()
                .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => new PositionDto(src.Geography.ToPosition().Latitude,
                        src.Geography.ToPosition().Longitude)));

            CreateMap<OrderItem, IOrderItemDto>()
                .ForMember(desc => desc.DiscountIsValid, opt => opt.ResolveUsing(CheckIsValidDiscount));

            CreateMap<OrderItemDiscountBase, IOrderItemDiscountBaseDto>()
                .Include<OrderItemPercentDiscount, IOrderItemPercentDiscountDto>();

            CreateMap<OrderItemPercentDiscount, IOrderItemPercentDiscountDto>();

            CreateMap<OrderProduct, IOrderProductDto>();
        }
        private OrderType GetOrderType(OrderBase orderBase)
        {
            if (orderBase is PrivateOrder)
            {
                return OrderType.PrivateOrder;
            }
            return OrderType.AreaOrder;
        }

        private int CalcTimeLeft(OrderBase order)
        {
            if (order.OrderStatus == OrderStatus.Pending)
            {
                return (int)order.ExpireOpenTime.Subtract(DateTime.Now).TotalMinutes > 0
                    ? (int)order.ExpireOpenTime.Subtract(DateTime.Now).TotalMinutes
                    : 0;
            }
            if (order.OrderStatus == OrderStatus.SelfOpened)
            {
                return (int)order.ResponseExpireTime.Value.Subtract(DateTime.Now).TotalMinutes > 0
                    ? (int)order.ResponseExpireTime.Value.Subtract(DateTime.Now).TotalMinutes
                    : 0;
            }
            if (order.OrderStatus == OrderStatus.HasSuggestion)
            {
                return (int)order.SuggestionTime.Value.Subtract(DateTime.Now).TotalMinutes > 0
                    ? (int)order.SuggestionTime.Value.Subtract(DateTime.Now).TotalMinutes
                    : 0;
            }
            return 0;
        }
        private bool CheckIsExpired(OrderBase orderBase)
        {
            var now = DateTime.Now;
            switch (orderBase.OrderStatus)
            {
                case OrderStatus.Pending when orderBase.ExpireOpenTime < now:
                case OrderStatus.SelfOpened when orderBase.ResponseExpireTime < now:
                case OrderStatus.OtherOpened when orderBase.ResponseExpireTime < now:
                case OrderStatus.HasSuggestion when orderBase.SuggestionTime < now:
                    return true;
                default:
                    return false;
            }
        }
        private static bool CheckIsValidDiscount(OrderItem orderItem)
        {
            return orderItem.Discount != null;
        }
        private static decimal CalcTotalPrice(OrderBase orderBase)
        {
            return orderBase.OrderItems.Sum(item => item.OrderProduct.Price * item.Quantity);
        }



    }
}