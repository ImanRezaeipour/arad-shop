using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum ShipmentState
    {
        [Description("معلق")]
        Pending=0,
        [Description("ارسال شده")]
        Send=1,
        [Description("تحویل")]
        Delivery=2,
        [Description("بازگشتی")]
        Reverse=3
    }
}