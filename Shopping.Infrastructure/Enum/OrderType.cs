using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum OrderType
    {
        [Description("سفارش شخصی")]
        PrivateOrder = 0,
        [Description("سفارش تبدیل شده")]
        AreaOrder = 1
    }
}