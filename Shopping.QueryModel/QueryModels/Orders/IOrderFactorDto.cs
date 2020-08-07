using Shopping.QueryModel.QueryModels.Factors;
using Shopping.QueryModel.QueryModels.Orders.Abstract;

namespace Shopping.QueryModel.QueryModels.Orders
{
    public interface IOrderFactorDto
    {
        IOrderBaseFullInfoDto Order { get; set; }
        IFactorDto Factor { get; set; }
    }
}