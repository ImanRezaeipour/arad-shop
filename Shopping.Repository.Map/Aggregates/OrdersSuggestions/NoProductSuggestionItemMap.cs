using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities;

namespace Shopping.Repository.Map.Aggregates.OrdersSuggestions
{
    public class NoProductSuggestionItemMap
        : EntityTypeConfiguration<NoProductSuggestionItem>
    {
        public NoProductSuggestionItemMap()
        {
            ToTable("NoProductSuggestionItem");
        }
    }
}