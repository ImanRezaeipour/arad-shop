using System;
using Shopping.Infrastructure.Enum.Accounting;

namespace Shopping.Scheduler.Core.Messages.Accounting.Details
{
    public class CreateDetailRequest
    {
        public long Code { get; set; }
        public Guid RequestId { get; set; }
        public string Name { get; set; }
        public string ClassCode { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public PersonTypeAccounting PersonType { get; set; }
        public string PostalCode { get; set; }
        public int Province { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string CommercialCode { get; set; }
        public int City { get; set; }
        public string Address { get; set; }
        public string Spec1 { get; set; }
    }
}