using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Categories
{
	public class CategoryMap:EntityTypeConfiguration<Category>
	{
		public CategoryMap()
		{
			ToTable("Category");
		}
	}
}