using System;
using Shopping.Infrastructure.Enum;
using Shopping.QueryModel.QueryModels.ShopAcceptors;

namespace Shopping.QueryModel.Implements.ShopAcceptors
{
    public class ShopAcceptorsDto: IShopAcceptorsDto
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string mobileNumber { get; set; }
        public string shopName { get; set; }
        public DateTime creationTime { get; set; }
        public ShopAcceptorStatus shopAcceptorStatus { get; set; }
    }
}