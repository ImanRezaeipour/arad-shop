using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.CustomerDiscountPurchases.Aggregates;

namespace Shopping.Repository.Map.Aggregates.CustomerDiscountPurchases
{
    public class CustomerDiscountPurchasMap : EntityTypeConfiguration<CustomerDiscountPurchase>
    {
        public CustomerDiscountPurchasMap()
        {
            HasKey(item => item.Id);
            Property(item => item.RowVersion).IsRowVersion();
        }
    }
}