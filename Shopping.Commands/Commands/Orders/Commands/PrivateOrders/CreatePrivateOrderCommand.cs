using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Orders.Commands.Abstract;
using Shopping.Commands.Commands.Orders.Validators;

namespace Shopping.Commands.Commands.Orders.Commands.PrivateOrders
{
    [Validator(typeof(CreatePrivateOrderCommandValidator))]
    public class CreatePrivateOrderCommand : OrderBaseCommand
    {
        public Guid ShopId { get; set; }
    }
}