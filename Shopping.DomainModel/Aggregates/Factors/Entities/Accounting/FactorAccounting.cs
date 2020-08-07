using System;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.Accounting
{
    public class FactorAccounting : Entity, IHasCreationTime
    {
        protected FactorAccounting() { }
        public FactorAccounting(Guid id, string gId)
        {
            Id = id;
            GRDBId = gId;
            CreationTime = DateTime.Now;
        }
        public string GRDBId { get; private set; }
        public DateTime CreationTime { get; private set; }
        public bool RecPay { get; set; }
        public DateTime? RecPayCreationTime { get; set; }
        public override void Validate()
        {
        }
    }
}