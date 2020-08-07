using System;
using Parbad.Core;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Payments.ValueObjects
{
    public class PaymentResponse : ValueObject<PaymentResponse>, IHasCreationTime
    {
        protected PaymentResponse() { }
        public PaymentResponse(string referenceId, string transactionId, string message, VerifyResultStatus? status)
        {
            ReferenceId = referenceId;
            TransactionId = transactionId;
            Message = message;
            Status = status;
            CreationTime = DateTime.Now;
        }
        public string ReferenceId { get; private set; }
        public string TransactionId { get; private set; }
        public string Message { get; private set; }
        public VerifyResultStatus? Status { get; private set; }
        public DateTime CreationTime { get; private set; }
        //for complex type
        public bool HasValue => (ReferenceId != null && TransactionId != null && Message != null && Status != null);
    }
}