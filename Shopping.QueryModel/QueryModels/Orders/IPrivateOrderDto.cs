using Shopping.QueryModel.QueryModels.Orders.Abstract;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.QueryModels.Orders
{
    public interface IPrivateOrderDto : IOrderBaseFullInfoDto
    {
        IShopDto Shop { get; set; }
        bool IsConvertToAreaOrder { get; set; }
        IAreaOrderDto AcceptedAreaOrder { get; set; }
    }
}