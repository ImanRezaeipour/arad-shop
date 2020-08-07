using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ShopCustomerSubsetSettlements.Aggregates;

namespace Shopping.Repository.Map.Aggregates.ShopCustomerSubsetSettlements
{
    public class ShopCustomerSubsetSettlingMap : EntityTypeConfiguration<ShopCustomerSubsetSettlement>
    {
        public ShopCustomerSubsetSettlingMap()
        {
            HasKey(p => p.Id);
        }
    }
}