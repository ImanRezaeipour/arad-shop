using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ProductsSuggestions.Aggregates;

namespace Shopping.Repository.Map.Aggregates.ProductsSuggestions.ShopProductsSuggestion
{
    public class ShopProductSuggestionMap:EntityTypeConfiguration<ShopProductSuggestion>
    {
        public ShopProductSuggestionMap()
        {
            ToTable("ShopProductSuggestion");
        }
    }
}