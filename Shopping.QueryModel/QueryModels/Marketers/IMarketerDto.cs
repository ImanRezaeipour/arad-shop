using System;

namespace Shopping.QueryModel.QueryModels.Marketers
{
    public interface IMarketerDto
    {
        /// <summary>
        /// شناسه
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// حداکثر تعداد مجاز فروشگاه برای بازاریابی
        /// </summary>
        int MaxMarketerAllowed { get; set; }
        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        DateTime CreationTime { get; set; }
        /// <summary>
        /// فعال/ غیرفعال
        /// </summary>
        bool IsActive { get; set; }
        /// <summary>
        /// تعداد فروشگاه های زیرمجموعه
        /// </summary>
        int SubsetShopCount { get; set; }
        string Image { get; set; }
        Guid BarcodeId { get; set; }
    }
}