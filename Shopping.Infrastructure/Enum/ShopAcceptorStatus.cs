using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum ShopAcceptorStatus
    {
        [Description("در حال بررسی")]
        Pending = 0,
        [Description("تایید")]
        Accept = 1,
        [Description("رد تایید")]
        Reject = 2
    }
}