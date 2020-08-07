using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Aggregates;

namespace Shopping.Repository.Map.Aggregates.OrdersSuggestions
{
    public class OrderSuggestionMap:EntityTypeConfiguration<OrderSuggestion>
    {
        public OrderSuggestionMap()
        {
            HasKey(p => p.Id);
            HasMany(p => p.OrderSuggestionItems);
        }
    }
}