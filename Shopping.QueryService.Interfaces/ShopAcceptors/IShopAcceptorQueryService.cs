using System;
using System.Linq;
using Shopping.QueryModel.QueryModels.ShopAcceptors;

namespace Shopping.QueryService.Interfaces.ShopAcceptors
{
    public interface IShopAcceptorQueryService
    {
        IQueryable<IShopAcceptorsDto> GetAll();
        IShopAcceptorsWithAddressDto GetById(Guid id);
    }
}