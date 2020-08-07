using System;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.ShopCustomerSubsetSettlements.Aggregates
{
    public class ShopCustomerSubsetSettlement : AggregateRoot, IHasCreationTime
    {
        protected ShopCustomerSubsetSettlement() { }
        public ShopCustomerSubsetSettlement(Guid id, Shop shop, UserInfo userInfo, decimal amount, ShopCustomerSubsetSettlementType type)
        {
            Id = id;
            Shop = shop;
            UserInfo = userInfo;
            Amount = amount;
            Type = type;
            CreationTime = DateTime.Now;
            IsRegisteredInAccounting = false;
        }
        public DateTime CreationTime { get; private set; }
        public virtual Shop Shop { get; private set; }
        public virtual UserInfo UserInfo { get; private set; }
        public decimal Amount { get; private set; }
        public ShopCustomerSubsetSettlementType Type { get; private set; }
        public bool IsRegisteredInAccounting { get; private set; }
        public string AccountingId { get; private set; }
        public void RegisterInAccounting(string accountingId)
        {
            AccountingId = accountingId;
            IsRegisteredInAccounting = true;
        }
        public override void Validate()
        {
        }
    }
}