using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Discounts.Aggregates.Abstract;

namespace Shopping.Repository.Map.Aggregates.Discounts
{
    public class DiscountBaseMap : EntityTypeConfiguration<DiscountBase>
    {
        public DiscountBaseMap()
        {
            HasKey(p => p.Id);
            HasMany(item => item.Sells);
        }
    }
}