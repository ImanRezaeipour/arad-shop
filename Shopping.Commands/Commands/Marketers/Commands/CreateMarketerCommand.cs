using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Marketers.Commands.CommandItems;
using Shopping.Commands.Commands.Marketers.Validators;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Marketers.Commands
{
    [Validator(typeof(CreateMarketerCommandValidator))]
    public class CreateMarketerCommand : ShoppingCommandBase
    {
            public Guid UserId { get; set; }
            public Guid BarcodeId { get; set; }
            /// <summary>
            /// نام
            /// </summary>
            public string FirstName { get; set; }
            /// <summary>
            /// نام خانوادگی
            /// </summary>
            public string LastName { get; set; }
            /// <summary>
            /// کدملی
            /// </summary>
            public string NationalCode { get; set; }
            /// <summary>
            /// شماره شناسنامه
            /// </summary>
            public string IdNumber { get; set; }
            /// <summary>
            /// جنسیت
            /// </summary>
            public Gender Gender { get; set; }
            /// <summary>
            /// نام پدر
            /// </summary>
            public string FatherName { get; set; }
            /// <summary>
            /// تاریخ تولد
            /// </summary>
            public DateTime BirthDate { get; set; }
            /// <summary>
            /// توضحیات
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// حداکثر تعداد مجاز فروشگاه برای بازاریابی
            /// </summary>
            public int MaxMarketerAllowed { get; set; }
            /// <summary>
            /// مدارک
            /// </summary>
            public List<string> Documents { get; set; }
            /// <summary>
            /// عکس بازاریاب
            /// </summary>
            public string Image { get; set; }
            /// <summary>
            /// آدرس بازاریاب
            /// </summary>
            public MarketerAddressCommandItem MarketerAddress { get; set; }
            /// <summary>
            /// حقوق بازارایب
            /// </summary>
            public MarketerSalaryCommandItem MarketerSalary { get; set; }
            /// <summary>
            /// معرف بازاریاب
            /// </summary>
            public MarketerReagentCommandItem MarketerReagent { get; set; }
    }
}