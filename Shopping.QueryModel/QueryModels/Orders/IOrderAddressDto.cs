namespace Shopping.QueryModel.QueryModels.Orders
{
    public interface IOrderAddressDto
    {
        string AddressText { get; set; }
        string PhoneNumber { get; set; }
        string CityName { get; set; }
        IPositionDto Position { get; set; }
    }
}