using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorWithShopDto: IFactorDto
    {
        IShopDto Shop { get; set; }
    }
}