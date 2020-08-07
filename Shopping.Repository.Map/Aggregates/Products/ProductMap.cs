using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Products.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Products
{
    public class ProductMap: EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(p => p.Id);
            HasMany(p => p.ProductImages);
        }
    }
}