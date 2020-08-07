using System.ComponentModel;

namespace Shopping.Infrastructure.Enum
{
    public enum CheckOrderStatus
    {
        [Description("در انتظار پاسخ فروشگاه")]
        PendingSendForShop = 0,

        [Description("باز شده توسط فروشگاه")]
        Opened = 1,

        [Description("عدم ثبت پیشنهاد فروشگاه")]
        NotAccepted = 2,

        [Description("فروشگاه پیشنهاد ثبت کرد")]
        ShopAddedSuggestion = 3,

        [Description("ارسال به بقیه فروشگاه ها")]
        SendForOtherShops = 4,

        [Description("باز شده توسط یکی از فروشگاه ها")]
        OtherOpened = 5,

        [Description("یکی از فروشگاه ها پیشنهاد ثبت کرده است")]
        OtherAddedSuggestion = 6,

        [Description("باز شده توسط یکی از فروشگاه ها و عدم ثبت پیشنهاد")]
        OtherNotAccepted = 7,

        [Description("توسط هیچ فروشگاهی باز نشد")]
        OtherNotOpened = 8,

        [Description("فروشگاه دیگری در محدوده نمی باشد")]
        AnyShopsNotAround = 9,

        [Description("زمانبند از کار افتاده")]
        SchedulerDoesNotWork = 100
    }
}