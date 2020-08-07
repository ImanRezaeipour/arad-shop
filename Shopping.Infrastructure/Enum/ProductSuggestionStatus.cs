using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum ProductSuggestionStatus
    {
        [Description("معلق")]
        Pending = 0,
        [Description("تایید شده")]
        Accept = 1,
        [Description("رد شده")]
        Reject = 2
    }
}