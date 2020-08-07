namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopWithAddressDto:IShopDto
    {
        IShopAddressDto ShopAddress { get; set; }
    }
}