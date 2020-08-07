using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount;

namespace Shopping.Repository.Map.Aggregates.Products
{
    public class ProductPercentDiscountMap : EntityTypeConfiguration<ProductPercentDiscount>
    {
        public ProductPercentDiscountMap()
        {
            ToTable("ProductPercentDiscount");
        }
    }
}