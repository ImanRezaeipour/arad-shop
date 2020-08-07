using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates.Abstrct;

namespace Shopping.Repository.Map.Aggregates.ProductsSuggestions
{
    public class ProductSuggestionMap:EntityTypeConfiguration<ProductSuggestion>
    {
        public ProductSuggestionMap()
        {
            HasKey(p => p.Id);
        } 
    }
}