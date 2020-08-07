using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ShopMarketersHistories.Aggregates;

namespace Shopping.Repository.Map.Aggregates.ShopMarketerHistories
{
    public class ShopMarketerHistoryMap : EntityTypeConfiguration<ShopMarketersHistory>
    {
        public ShopMarketerHistoryMap()
        {
            HasKey(item => item.Id);
        }
    }
}