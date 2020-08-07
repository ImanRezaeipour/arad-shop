using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum SettlingState
    {
        [Description("معلق")]
        Pending,
        [Description("پرداخت شده")]
        Paid,
        [Description("پرداخت نشده")]
        Unpaid
    }
}