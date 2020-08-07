using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Products.Entities.ProductDiscount;

namespace Shopping.Repository.Map.Aggregates.Products
{
    public class ProductDiscountBaseMap : EntityTypeConfiguration<ProductDiscountBase>
    {
        public ProductDiscountBaseMap()
        {
            HasKey(p => p.Id);
        }
    }
}