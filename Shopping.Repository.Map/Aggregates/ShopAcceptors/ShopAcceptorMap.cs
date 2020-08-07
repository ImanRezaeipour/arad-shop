using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ShopAcceptors.Aggregates;

namespace Shopping.Repository.Map.Aggregates.ShopAcceptors
{
    public class ShopAcceptorMap:EntityTypeConfiguration<ShopAcceptor>
    {
        public ShopAcceptorMap()
        {
            HasKey(p => p.Id);
        }
    }
}