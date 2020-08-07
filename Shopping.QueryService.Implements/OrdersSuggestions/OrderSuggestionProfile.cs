using System.Linq;
using AutoMapper;
using Shopping.DomainModel.Aggregates.Orders.Entities.Discounts;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.ValueObjects;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.Implements.OrdersSuggestions;
using Shopping.QueryModel.QueryModels.OrdersSuggestions;
using Shopping.QueryModel.QueryModels.OrdersSuggestions.Abstract;

namespace Shopping.QueryService.Implements.OrdersSuggestions
{
    public class OrderSuggestionProfile : Profile
    {
        public OrderSuggestionProfile()
        {
            CreateMap<OrderSuggestion, IOrderSuggestionDto>()
                .ForMember(des => des.TotalPrice, opt => opt.ResolveUsing(CalcTotalPrice))
                .ForMember(des => des.DiscountTotalPrice, opt => opt.ResolveUsing(CalcDiscountTotalPrice))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => CreateOrderNumber(src.OrderId)))
                .ForMember(dest => dest.OrderSuggestionItemTypeCount, opt => opt.ResolveUsing(GetOrderSuggestionItemTypeCount));

            CreateMap<OrderSuggestionItemBase, IOrderSuggestionItemBaseDto>()
                .Include<AlternativeProductSuggestionItem, IAlternativeProductSuggestionItemDto>()
                .Include<HasProductSuggestionItem, IHasProductSuggestionItemDto>()
                .Include<NoProductSuggestionItem, INoProductSuggestionItemDto>()
                .ForMember(des => des.OrderSuggestionItemType, opt => opt.ResolveUsing(GetOrderSuggestionItemType));

            CreateMap<HasProductSuggestionItem, IHasProductSuggestionItemDto>();
            CreateMap<AlternativeProductSuggestionItem, IAlternativeProductSuggestionItemDto>();
            CreateMap<NoProductSuggestionItem, INoProductSuggestionItemDto>();

            CreateMap<AlternativeProductSuggestion, IAlternativeProductSuggestionDto>();
        }

        private OrderSuggestionItemType GetOrderSuggestionItemType(OrderSuggestionItemBase orderSuggestionItemBase)
        {
            if (orderSuggestionItemBase is AlternativeProductSuggestionItem)
            {
                return OrderSuggestionItemType.AlternativeProduct;
            }
            if (orderSuggestionItemBase is HasProductSuggestionItem)
            {
                return OrderSuggestionItemType.HasProduct;
            }
            return OrderSuggestionItemType.NoProduct;
        }
        private string CreateOrderNumber(long orderId) =>
            $"ord-{orderId:D8}";

        private IOrderSuggestionItemTypeCountDto GetOrderSuggestionItemTypeCount(OrderSuggestion orderSuggestion)
        {
            var result = new OrderSuggestionItemTypeCountDto
            {
                AcceptedCount = orderSuggestion.OrderSuggestionItems.OfType<HasProductSuggestionItem>().Count(),
                RejectedCount = orderSuggestion.OrderSuggestionItems.OfType<NoProductSuggestionItem>().Count(),
                AlternativeCount = orderSuggestion.OrderSuggestionItems.OfType<AlternativeProductSuggestionItem>().Count()
            };
            return result;
        }
        private decimal CalcTotalPrice(OrderSuggestion orderSuggestion)
        {
            decimal totalPrice = 0;
            foreach (var item in orderSuggestion.OrderSuggestionItems)
            {
                if (item is AlternativeProductSuggestionItem)
                {
                    var alternativeProductSuggestionItem = (AlternativeProductSuggestionItem)item;
                    totalPrice += alternativeProductSuggestionItem.Price * alternativeProductSuggestionItem.Quantity;
                }
                if (item is HasProductSuggestionItem)
                {
                    var hasProductSuggestionItem = (HasProductSuggestionItem)item;
                    totalPrice += hasProductSuggestionItem.Price * hasProductSuggestionItem.Quantity;
                }
            }
            return decimal.Round(totalPrice);
        }
        private decimal CalcDiscountTotalPrice(OrderSuggestion orderSuggestion)
        {
            decimal totalPrice = 0;
            foreach (var item in orderSuggestion.OrderSuggestionItems)
            {
                if (item is AlternativeProductSuggestionItem alternativeProductSuggestionItem)
                {
                    if (alternativeProductSuggestionItem.OrderItem.Discount != null)
                    {
                        if (alternativeProductSuggestionItem.OrderItem.Discount is OrderItemPercentDiscount percentDiscount)
                        {
                            if (alternativeProductSuggestionItem.OrderItem.Discount.HasDiscountValid())
                            {
                                var price = alternativeProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                                totalPrice += price * (decimal)(100 - percentDiscount.Percent) / 100;

                                if (alternativeProductSuggestionItem.Quantity - 1 > 0)
                                {
                                    var price1 = alternativeProductSuggestionItem.Price * (100 - orderSuggestion.Discount) /
                                                 100;
                                    totalPrice += price1 * (alternativeProductSuggestionItem.Quantity - 1);
                                }
                            }
                            else
                            {
                                var price = alternativeProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                                totalPrice += price * alternativeProductSuggestionItem.Quantity;
                            }
                        }
                        else
                        {
                            var price = alternativeProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                            totalPrice += price * alternativeProductSuggestionItem.Quantity;
                        }
                    }
                    else
                    {
                        var price = alternativeProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                        totalPrice += price * alternativeProductSuggestionItem.Quantity;
                    }
                }
                if (item is HasProductSuggestionItem hasProductSuggestionItem)
                {
                    if (hasProductSuggestionItem.OrderItem.Discount != null)
                    {
                        if (hasProductSuggestionItem.OrderItem.Discount is OrderItemPercentDiscount percentDiscount)
                        {
                            if (hasProductSuggestionItem.OrderItem.Discount.HasDiscountValid())
                            {
                                var price = hasProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                                totalPrice += price * (decimal)(100 - percentDiscount.Percent) / 100;

                                if (hasProductSuggestionItem.Quantity - 1 > 0)
                                {
                                    var price1 = hasProductSuggestionItem.Price * (100 - orderSuggestion.Discount) /
                                                 100;
                                    totalPrice += price1 * (hasProductSuggestionItem.Quantity - 1);
                                }
                            }
                            else
                            {
                                var price = hasProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                                totalPrice += price * hasProductSuggestionItem.Quantity;
                            }
                        }
                        else
                        {
                            var price = hasProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                            totalPrice += price * hasProductSuggestionItem.Quantity;
                        }
                    }
                    else
                    {
                        var price = hasProductSuggestionItem.Price * (100 - orderSuggestion.Discount) / 100;
                        totalPrice += price * hasProductSuggestionItem.Quantity;
                    }
                }
            }
            return decimal.Round(totalPrice);
        }
    }
}