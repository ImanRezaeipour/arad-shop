using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.FakeIos.Customers
{
    public class FakeCustomerIos : AggregateRoot
    {
        protected FakeCustomerIos(){}
        public FakeCustomerIos(Guid id, string fullName, string username, string password)
        {
            Id = id;
            FullName = fullName;
            Username = username;
            Password = password;
            LoginCount = 0;
        }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int LoginCount { get; set; }
        public override void Validate()
        {
        }
    }
}