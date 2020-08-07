using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;

namespace Shopping.Repository.Map.Aggregates.Categories
{
    public class CategoryBaseMap : EntityTypeConfiguration<CategoryBase>
    {
        public CategoryBaseMap()
        {
            HasKey(item => item.Id);
        }
    }
}