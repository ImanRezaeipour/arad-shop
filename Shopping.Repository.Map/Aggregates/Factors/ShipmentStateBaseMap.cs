using System.Data.Entity.ModelConfiguration;
using Shopping.DomainModel.Aggregates.Factors.Entities.ShipmentState;

namespace Shopping.Repository.Map.Aggregates.Factors
{
    public class ShipmentStateBaseMap : EntityTypeConfiguration<ShipmentStateBase>
    {
        public ShipmentStateBaseMap()
        {
            HasKey(p => p.Id);
        }
    }
    public class DeliveryShipmentStateMap : EntityTypeConfiguration<DeliveryShipmentState>
    {
        public DeliveryShipmentStateMap()
        {
            ToTable("DeliveryShipmentState");
        }
    }
    public class PendingShipmentStateMap : EntityTypeConfiguration<PendingShipmentState>
    {
        public PendingShipmentStateMap()
        {
            ToTable("PendingShipmentState");
        }
    }
    public class ReverseShipmentStateMap : EntityTypeConfiguration<ReverseShipmentState>
    {
        public ReverseShipmentStateMap()
        {
            ToTable("ReverseShipmentState");
        }
    }
    public class SendShipmentStateMap : EntityTypeConfiguration<SendShipmentState>
    {
        public SendShipmentStateMap()
        {
            ToTable("SendShipmentState");
        }
    }
}