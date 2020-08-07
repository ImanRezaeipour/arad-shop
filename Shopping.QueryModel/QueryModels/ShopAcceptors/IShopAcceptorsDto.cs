using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.ShopAcceptors
{
    public interface IShopAcceptorsDto
    {
        Guid id { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        string phoneNumber { get; set; }
        string mobileNumber { get; set; }
        string shopName { get; set; }
        DateTime creationTime { get; set; }
        ShopAcceptorStatus shopAcceptorStatus { get; set; }
    }
}