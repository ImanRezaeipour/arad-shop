using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum AreaOrderCreator
    {
        [Description("توسط مشتری")]
        ByCustomer=0,
        [Description("توسط سیستم")]
        ByScheduler = 1
    }
}