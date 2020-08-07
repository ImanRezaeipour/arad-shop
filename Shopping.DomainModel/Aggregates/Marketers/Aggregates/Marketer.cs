using System;
using System.Collections.Generic;
using Shopping.DomainModel.Aggregates.Marketers.Entities;
using Shopping.DomainModel.Aggregates.Marketers.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Marketers.Aggregates
{
    /// <summary>
    /// بازاریاب
    /// </summary>
    public class Marketer : AggregateRoot<long>, IHasCreationTime, IPassivable
    {
        protected Marketer()
        {
        }
        public Marketer(Guid barcodeId, string firstName, string lastName, string nationalCode,
            string idNumber, Gender gender, string fatherName, DateTime birthDate,
            string description, int maxMarketerAllowed, IList<string> documents, string image,
            MarketerAddress marketerAddress, MarketerSalary marketerSalary, MarketerReagent marketerReagent,
            Guid userId)
        {
            BarcodeId = barcodeId;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            IdNumber = idNumber;
            Gender = gender;
            FatherName = fatherName;
            BirthDate = birthDate;
            Description = description;
            MaxMarketerAllowed = maxMarketerAllowed;
            Image = image;
            Documents = string.Join(",", documents);
            MarketerAddress = marketerAddress;
            MarketerSalary = marketerSalary;
            MarketerReagent = marketerReagent;
            UserId = userId;
            CreationTime = DateTime.Now;
            IsActive = true;
        }
        /// <summary>
        /// شناسه بارکد
        /// </summary>
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
        /// تاریخ ثبت
        /// </summary>
        public DateTime CreationTime { get; private set; }
        /// <summary>
        /// فعال/ غیرفعال
        /// </summary>
        public bool IsActive { get; private set; }
        /// <summary>
        /// حداکثر تعداد مجاز فروشگاه برای بازاریابی
        /// </summary>
        public int MaxMarketerAllowed { get; set; }
        /// <summary>
        /// مدارک
        /// </summary>
        public string Documents { get; set; }
        /// <summary>
        /// عکس بازاریاب
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// آدرس بازاریاب
        /// </summary>
        public virtual MarketerAddress MarketerAddress { get; set; }
        /// <summary>
        /// حقوق بازارایب
        /// </summary>
        public virtual MarketerSalary MarketerSalary { get; set; }
        /// <summary>
        /// معرف بازاریاب
        /// </summary>
        public virtual MarketerReagent MarketerReagent { get; set; }
        public Guid UserId { get; private set; }
        public virtual ICollection<MarketerComment> Comments { get; set; }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public override void Validate()
        {
        }
    }
}