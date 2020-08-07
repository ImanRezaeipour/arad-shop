using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum PublicMessageType
    {
        [Description("پیام برای فروشگاه")]
        ShopMessage=0,
        [Description("پیام برای مشتری")]
        CustomerMessage=1
    }
}