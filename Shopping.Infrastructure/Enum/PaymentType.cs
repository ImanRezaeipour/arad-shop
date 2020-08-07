using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum PaymentType
    {
        [Description("معلق")]
        Pending = 0,
        [Description("پرداخت شده")]
        Paid = 1,
        [Description("ناموفق")]
        Failed = 2,
        [Description("بازگشت تراکنش")]
        Reverse = 3
    }
}