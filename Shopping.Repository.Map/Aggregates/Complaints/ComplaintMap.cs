using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Complaints.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Complaints
{
    public class ComplaintMap : EntityTypeConfiguration<Complaint>
    {
        public ComplaintMap()
        {
            HasKey(item => item.Id);
        }
    }
}