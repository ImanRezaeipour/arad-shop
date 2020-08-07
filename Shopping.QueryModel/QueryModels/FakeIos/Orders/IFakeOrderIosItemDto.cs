using System;

namespace Shopping.QueryModel.QueryModels.FakeIos.Orders
{
    public interface IFakeOrderIosItemDto
    {
        Guid Id { get; set; }
        Guid ProductId { get; set; }
        string Name { get; set; }
        string Image { get; set; } 
        string Brand { get; set; }
        int Quantity { get; set; } 
    }
}