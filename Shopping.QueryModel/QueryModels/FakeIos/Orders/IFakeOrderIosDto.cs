using System;
using System.Collections.Generic;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.FakeIos.Orders
{
    public interface IFakeOrderIosDto
    {
        Guid Id { get; set; }
        string FullName { get; set; }
        Guid CustomerId { get; set; }
        string AddressText { get; set; }
        double AddressLat { get; set; }
        double AddressLng { get; set; }
        string CreationTime { get; set; }
        FakeOrderIosState State { get; set; }
        IList<IFakeOrderIosItemDto> Items { get; set; }
    }
}