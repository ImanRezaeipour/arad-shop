namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorAddressDto
    {
        string AddressText { get; set; }
        string PhoneNumber { get; set; }
        string CityName { get; set; }
        IPositionDto Position { get; set; }
    }
}