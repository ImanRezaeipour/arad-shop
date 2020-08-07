using System;
using System.Collections.Generic;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.FakeIos.Orders
{
    public class FakeOrderIos : AggregateRoot, IHasCreationTime
    {
        protected FakeOrderIos() { }
        public FakeOrderIos(Guid id, string fullName, Guid customerId, string addressText, double addressLat, double addressLng,
             ICollection<FakeOrderIosItem> items)
        {
            Id = id;
            Items = items;
            FullName = fullName;
            CustomerId = customerId;
            AddressText = addressText;
            AddressLat = addressLat;
            AddressLng = addressLng;
            State = FakeOrderIosState.Pending;
            CreationTime = DateTime.Now;
        }
        public string FullName { get; set; }
        public Guid CustomerId { get; set; }
        public string AddressText { get; set; }
        public double AddressLat { get; set; }
        public double AddressLng { get; set; }
        public DateTime CreationTime { get; set; }
        public FakeOrderIosState State { get; set; }
        public virtual ICollection<FakeOrderIosItem> Items { get; set; }
        public override void Validate()
        {
        }
    }
}