using System.ComponentModel;

namespace Shopping.Shared.Enums
{
    public enum AppType
    {
        [Description("مشتری")]
        Customer = 0,
        [Description("فروشگاه")]
        Shop = 1
    }
}