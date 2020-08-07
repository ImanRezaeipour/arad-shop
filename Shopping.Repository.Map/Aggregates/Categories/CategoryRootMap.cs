using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;

namespace Shopping.Repository.Map.Aggregates.Categories
{
	public class CategoryRootMap:EntityTypeConfiguration<CategoryRoot>
	{
		public CategoryRootMap()
		{
			ToTable("CategoryRoot");
		}
	}
}