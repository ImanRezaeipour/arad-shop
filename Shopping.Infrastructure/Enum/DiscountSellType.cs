using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum DiscountSellType
    {
        [Description("معمولی")]
        Usual,
        [Description("خریداولی ها")]
        FirstPurchase
    }
}