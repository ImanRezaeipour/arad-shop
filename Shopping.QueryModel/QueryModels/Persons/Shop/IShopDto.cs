using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.Persons.Abstract;

namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopDto : IPersonDto
    {
        string Name { get; set; }
        ShopStatus ShopStatus { get; set; }
        string DescriptionStatus { get; set; }
        string Description { get; set; }
        string NationalCode { get; set; }
        int DefaultDiscount { get; set; }
        DateTime CreationTime { get; set; }
        long? MarketerId { get; set; }
        int AreaRadius { get; set; }
        int Metrage { get; set; }
    }
}