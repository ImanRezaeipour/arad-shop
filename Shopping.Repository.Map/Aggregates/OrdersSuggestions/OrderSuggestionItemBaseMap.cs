using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.OrdersSuggestions.Entities.Abstract;

namespace Shopping.Repository.Map.Aggregates.OrdersSuggestions
{
    public class OrderSuggestionItemBaseMap
        : EntityTypeConfiguration<OrderSuggestionItemBase>
    {
        public OrderSuggestionItemBaseMap()
        {
            HasKey(p => p.Id);
            ToTable("OrderSuggestionItemBase");
        }
    }
}