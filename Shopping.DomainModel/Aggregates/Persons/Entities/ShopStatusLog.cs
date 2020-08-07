using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Persons.Entities
{
    public class ShopStatusLog : Entity
    {
        private ShopStatusLog()
        { }

        public ShopStatusLog(Guid id, Guid userId, string firstName, string lastName, ShopStatus shopStatus)
        {
            Id = id;
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ShopStatus = shopStatus;
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ShopStatus ShopStatus { get; set; }
        public override void Validate()
        {

        }
    }
}