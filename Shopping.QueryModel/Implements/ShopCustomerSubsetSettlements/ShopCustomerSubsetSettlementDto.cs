using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.Implements.ShopCustomerSubsetSettlements
{
    public class ShopCustomerSubsetSettlementDto
    {
        public DateTime CreationTime { get; set; }
        public decimal Amount { get; set; }
        public UserInfoDto UserInfo { get; set; }
        public ShopCustomerSubsetSettlementType Type { get; set; }
        public bool IsRegisteredInAccounting { get; set; }
        public string AccountingId { get; set; }
    }
}