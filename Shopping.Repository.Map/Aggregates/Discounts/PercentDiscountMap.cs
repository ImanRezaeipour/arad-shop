using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Discounts
{
    public class PercentDiscountMap : EntityTypeConfiguration<PercentDiscount>
    {
        public PercentDiscountMap()
        {
            ToTable("PercentDiscount");
            HasMany(p => p.ProductDiscounts);
            Property(p => p.Timestamp).IsRowVersion();
        }
    }
}