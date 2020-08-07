using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopStatusLogDto
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        ShopStatus ShopStatus { get; set; }
    }
}