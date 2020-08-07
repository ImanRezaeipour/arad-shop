using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Brands.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Brands
{
    public class BrandMap : EntityTypeConfiguration<Brand>
    {
        /// <summary>Initializes a new instance of EntityTypeConfiguration</summary>
        public BrandMap()
        {
            HasKey(item => item.Id);
        }
    }
}