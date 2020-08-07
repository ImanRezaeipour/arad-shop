using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.Aggregates;

namespace Shopping.Repository.Map.Aggregates.MarketerSalaryPayments
{
    public class MarketerSalaryPaymentMap:EntityTypeConfiguration<MarketerSalaryPayment>
    {
        public MarketerSalaryPaymentMap()
        {
            HasKey(p => p.Id);
        }
    }
}