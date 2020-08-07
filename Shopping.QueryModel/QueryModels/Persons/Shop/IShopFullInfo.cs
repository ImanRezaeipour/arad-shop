namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopFullInfo : IShopDto
    {
        IShopAddressDto ShopAddress { get; set; }
        IBankAccountDto BankAccount { get; set; }
        IImageDocumentsDto ImageDocuments { get; set; }
        long RecommendCode { get; set; }
    }
}