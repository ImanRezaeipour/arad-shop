namespace Shopping.QueryModel.QueryModels.ShopAcceptors
{
    public interface IShopAcceptorsWithAddressDto: IShopAcceptorsDto
    {
        IShopAcceptorAddressDto ShopAcceptorAddress { get; set; }
    }
}