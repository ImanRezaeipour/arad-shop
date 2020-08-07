using System;
using Shopping.DomainModel.Aggregates.Payments.ValueObjects;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Enum;

namespace Shopping.DomainModel.Aggregates.Payments.Aggregates
{
    public class Payment : AggregateRoot<long>, IHasCreationTime
    {
        protected Payment()
        {
        }
        public Payment(GatewayIpg gatewayIpg, long amount, long factorId)
        {
            GatewayIpg = gatewayIpg;
            Amount = amount;
            FactorId = factorId;
            CreationTime = DateTime.Now;
            PaymentType = PaymentType.Pending;
        }
        public PaymentType PaymentType { get; set; }
        public DateTime CreationTime { get; private set; }
        public GatewayIpg GatewayIpg { get; private set; }
        public long Amount { get; private set; }
        public long FactorId { get; private set; }
        public virtual PaymentResponse PaymentResponse { get; set; }
        public override void Validate()
        {
        }
    }
}