using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum FactorState
    {
        [Description("معلق")]
        Pending = 0,
        [Description("پرداخت شده")]
        Paid = 1,
        [Description("ناموفق")]
        Failed = 2
    }
}