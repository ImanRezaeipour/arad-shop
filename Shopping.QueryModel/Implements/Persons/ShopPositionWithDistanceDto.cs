using System;
using Shopping.QueryModel.QueryModels;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.Implements.Persons
{
    public class ShopPositionWithDistanceDto : IShopPositionWithDistanceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IPositionDto Position { get; set; }
        public int DefultDiscount { get; set; }
        public double? Distance { get; set; }
    }
}