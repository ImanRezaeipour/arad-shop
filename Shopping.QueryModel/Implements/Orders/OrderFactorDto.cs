using Shopping.QueryModel.QueryModels.Factors;
using Shopping.QueryModel.QueryModels.Orders;
using Shopping.QueryModel.QueryModels.Orders.Abstract;

namespace Shopping.QueryModel.Implements.Orders
{
    public class OrderFactorDto: IOrderFactorDto
    {
        public IOrderBaseFullInfoDto Order { get; set; }
        public IFactorDto Factor { get; set; }
    }
}