using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum OrderSuggestionStatus
    {
        [Description("معلق")]
        Pending = 0,
        [Description("قبول")]
        Accept = 1,
        [Description("رد")]
        Reject = 2
    }
}