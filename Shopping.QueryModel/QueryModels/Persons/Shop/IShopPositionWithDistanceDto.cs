namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopPositionWithDistanceDto : IShopPositionDto
    {
        double? Distance { get; set; }
    }
}