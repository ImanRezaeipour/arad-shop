using System.Collections.Generic;
using Shopping.Infrastructure.Enum;

namespace Shopping.QueryModel.QueryModels.Marketers
{
    public interface IMarketerFullInfoDto : IMarketerDto
    {
        /// <summary>
        /// کدملی
        /// </summary>
        string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        string IdNumber { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        Gender Gender { get; set; }
        /// <summary>
        /// نام پدر
        /// </summary>
        string FatherName { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        string BirthDate { get; set; }
        /// <summary>
        /// توضحیات
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// مدارک
        /// </summary>
        List<string> Documents { get; set; }
        /// <summary>
        /// آدرس بازاریاب
        /// </summary>
        IMarketerAddressDto MarketerAddress { get; set; }
        /// <summary>
        /// حقوق بازارایب
        /// </summary>
        IMarketerSalaryDto MarketerSalary { get; set; }
        /// <summary>
        /// معرف بازاریاب
        /// </summary>
        IMarketerReagentDto MarketerReagent { get; set; }
    }
}