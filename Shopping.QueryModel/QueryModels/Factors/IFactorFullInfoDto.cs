using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.Factors.FactorStates;
using Shopping.QueryModel.QueryModels.Persons.Customer;
using Shopping.QueryModel.QueryModels.Persons.Shop;

namespace Shopping.QueryModel.QueryModels.Factors
{
    public interface IFactorFullInfoDto : IFactorDto
    {
        ICustomerDto Customer { get; set; }
        IShopDto Shop { get; set; }
        IFactorAddressDto FactorAddress { get; set; }
        IList<IFactorRawDto> FactorRaws { get; set; }
        IFactorStateBaseDto IpgResult { get; set; }
    }
}