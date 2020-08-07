using System.Collections.Generic;

namespace Shopping.QueryModel.QueryModels.Persons.Shop
{
    public interface IShopWithLogDto
    {
        string Name { get; set; }
        IList<IShopStatusLogDto> ShopStatusLogs { get; set; }
    }
}