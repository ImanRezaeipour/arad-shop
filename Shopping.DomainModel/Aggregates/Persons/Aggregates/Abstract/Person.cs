using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.DomainModel.Aggregates.Persons.Entities;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract
{
    public abstract class Person : AggregateRoot<Guid>, IPassivable
    {
        protected Person() { }
        protected Person(Guid id, string firstName, string lastName, string emailAddress, Guid userId, string mobileNumber, long personNumber)
        {
            Id = id;
            MobileNumber = mobileNumber;
            PersonNumber = personNumber;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            UserId = userId;
            RegisterDate = DateTime.Now;
            IsActive = true;
        }
        public long PersonNumber { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public Guid UserId { get; set; }
        public bool IsActive { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public virtual PersonAccounting Accounting { get; set; }
        public virtual ICollection<AppInfo> AppInfos { get; set; }
        public List<NotifyMessage> GetPushTokens()
        {
            return AppInfos.Select(item => new NotifyMessage
            {
                RegistrationId = item.PushToken,
                OsType = item.OsType
            }).ToList();
        }
        public void SetAppInfo(OsType osType, string pushToken, string authDeviceId)
        {
            if (AppInfos.Any(p => p.PushToken == pushToken && p.OsType == osType))
            {
                return;
            }
            var appInfo = AppInfos.SingleOrDefault(p => p.AuthDeviceId == authDeviceId);
            if (appInfo != null)
            {
                appInfo.PushToken = pushToken;
                appInfo.OsType=osType;
            }
            else
            {
                AppInfos.Add(new AppInfo(Guid.NewGuid(), osType, pushToken, authDeviceId));
            }
        }
        public void Active() => IsActive = true;
        public void DeActive() => IsActive = false;
        public void RegisterInAccountingSystem(long detailCode) =>
            Accounting = new PersonAccounting(Guid.NewGuid(), detailCode);
    }
}