using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum OrderSuggestionItemType
    {
        [Description("دارای محصول")]
        HasProduct = 0,
        [Description("محصول جایگزین")]
        AlternativeProduct = 1,
        [Description("بدون محصول جایگزین")]
        NoProduct = 2
    }
}