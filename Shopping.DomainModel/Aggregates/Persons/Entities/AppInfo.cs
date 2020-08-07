using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Persons.Entities
{
    public class AppInfo : Entity
    {
        protected AppInfo() { }
        public AppInfo(Guid id, OsType osType, string pushToken, string authDeviceId)
        {
            Id = id;
            OsType = osType;
            PushToken = pushToken;
            AuthDeviceId = authDeviceId;
            Mute = false;
        }
        public OsType OsType { get;  set; }
        public string PushToken { get;  set; }
        public string AuthDeviceId { get; private set; }
        public bool Mute { get; private set; }
        public void MuteApp() => Mute = true;
        public void UnMuteApp() => Mute = false;
        public override void Validate()
        {
        }
    }
}