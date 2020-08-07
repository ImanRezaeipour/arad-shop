using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum ShopMarketersHistoryType
    {
        [Description("انتساب")]
        Assignment = 0,
        [Description("منفصل")]
        Detached = 1
    }
}