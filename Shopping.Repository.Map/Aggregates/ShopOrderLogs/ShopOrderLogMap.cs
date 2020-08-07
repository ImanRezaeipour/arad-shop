using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.ShopOrderLogs.Aggregates;

namespace Shopping.Repository.Map.Aggregates.ShopOrderLogs
{
    public class ShopOrderLogMap : EntityTypeConfiguration<ShopOrderLog>
    {
        public ShopOrderLogMap()
        {
            HasKey(p => p.Id);
        }
    }
}