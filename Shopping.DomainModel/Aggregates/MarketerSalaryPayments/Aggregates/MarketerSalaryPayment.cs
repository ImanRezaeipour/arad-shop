using System;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.ValueObjects;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.MarketerSalaryPayments.Aggregates
{
    public class MarketerSalaryPayment : AggregateRoot, IHasCreationTime
    {
        protected MarketerSalaryPayment()
        { }

        public MarketerSalaryPayment(Guid id, decimal amount, Marketer marketer, PeriodSalary periodSalary, UserInfo userInfo)
        {
            Id = id;
            CreationTime = DateTime.Now;
            Amount = amount;
            Marketer = marketer;
            PeriodSalary = periodSalary;
            UserInfo = userInfo;
        }
        public decimal Amount { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual Marketer Marketer { get; set; }
        public virtual PeriodSalary PeriodSalary { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public override void Validate()
        {
        }
    }
}