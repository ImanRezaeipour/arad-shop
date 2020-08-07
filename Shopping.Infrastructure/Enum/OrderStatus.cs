using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum OrderStatus
    {
        [Description("درانتظار")]
        Pending = 0,
        [Description("دارای پیشنهاد")]
        HasSuggestion = 1,
        [Description("قبول")]
        Accept = 2,
        [Description("باز شده توسط خودم")]
        SelfOpened = 3,
        [Description("باز شده توسط دیگران")]
        OtherOpened = 4,
        [Description("کنسل")]
        Cancel = 5
    }
}