using System;

namespace Shopping.QueryModel.QueryModels.Marketers
{
    public interface IMarketerAddressDto
    {
        string AddressText { get; set; }
        /// <summary>
        /// شماره ثابت
        /// </summary>
        string PhoneNumber { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        string MobileNumber { get; set; }
        /// <summary>
        /// شماره تلفن ضروری
        /// </summary>
        string RequiredPhoneNumber { get; set; }
        /// <summary>
        /// شناسه شهر
        /// </summary>
        Guid CityId { get; set; }
        /// <summary>
        /// نام شهر
        /// </summary>
        string CityName { get; set; }
    }
}