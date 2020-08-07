using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum SliderType
    {
        [Description("اسلایدر معمولی")]
        Slider=0,
        [Description("تخفیف 99 درصد")]
        Disount99=1
    }
}