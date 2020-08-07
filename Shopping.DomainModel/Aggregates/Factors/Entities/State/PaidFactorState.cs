using System;
using Parbad.Core;

namespace Shopping.DomainModel.Aggregates.Factors.Entities.State
{
    public class PaidFactorState : FactorStateBase
    {
        protected PaidFactorState() { }

        public PaidFactorState(Guid id, string referenceId, string transactionId, VerifyResultStatus status, string message) : base(id)
        {
            ReferenceId = referenceId;
            TransactionId = transactionId;
            Status = status;
            Message = message;
            PayTime=DateTime.Now;
        }

        public DateTime PayTime { get; private set; }

        public string ReferenceId { get; private set; }

        public string TransactionId { get; private set; }

        public VerifyResultStatus Status { get; private set; }

        public string Message { get; private set; }
        public override void Validate()
        {
        }
    }
}