namespace Shopping.QueryModel.QueryModels.Factors.ShipmentStates
{
    public interface IReverseShipmentStateDto: IShipmentStateBaseDto
    {
         string Reason { get; set; }
    }
}