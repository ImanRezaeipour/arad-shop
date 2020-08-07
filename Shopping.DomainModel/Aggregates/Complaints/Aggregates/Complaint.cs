using System;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Complaints.Aggregates
{
    public class Complaint:AggregateRoot,IHasCreationTime
    {
        protected Complaint()
        {}
        public Complaint(Guid id, string firstName, string lastName, string email, string title, string description, Shop shop)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Title = title;
            Description = description;
            CreationTime=DateTime.Now;
            Shop = shop;
        }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Shop Shop { get; set; }
        public bool Tracked { get; set; }
        public DateTime CreationTime { get; set; }
        public override void Validate()
        {
            
        }

       
    }
}