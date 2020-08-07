using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum ShopCustomerSubsetSettlementType
    {
        [Description("جذب مشتری")]
        RegisterCustomer,
        [Description("جذب مشتری منجر به خرید")]
        PaidFactor
    }
}