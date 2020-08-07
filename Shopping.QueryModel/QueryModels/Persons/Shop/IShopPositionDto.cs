using System;

namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopPositionDto
    {
        Guid Id { get; set; }
        string Name { get; set; }
        IPositionDto Position { get; set; }
        int DefultDiscount { get; set; }
    }
}