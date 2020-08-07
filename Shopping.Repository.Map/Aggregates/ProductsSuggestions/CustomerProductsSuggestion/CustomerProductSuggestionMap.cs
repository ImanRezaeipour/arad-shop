using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates;

namespace Shopping.Repository.Map.Aggregates.ProductsSuggestions.CustomerProductsSuggestion
{
    public class CustomerProductSuggestionMap:EntityTypeConfiguration<CustomerProductSuggestion>
    {
        public CustomerProductSuggestionMap()
        {
            ToTable("CustomerProductSuggestion");
        }
    }
}