using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Discounts.Commands;
using Shopping.Commands.Commands.Discounts.Responses;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.Discounts;

namespace Shopping.ApiService.Controllers.Controllers.Discounts
{
    [Authorize(Roles = "Support,Admin")]
    public class ProductPercentDiscountController : ApiControllerBase
    {
        private readonly IPercentDiscountQueryService _discountQueryService;
        public ProductPercentDiscountController(ICommandBus bus, IPercentDiscountQueryService discountQueryService) : base(bus)
        {
            _discountQueryService = discountQueryService;
        }
        public async Task<IHttpActionResult> Post(AddProductToPercentDiscountCommand command)
        {
            command.UserInfoCommand = new UserInfoCommandItem(UserId, FirstName, LastName);
            var response =
               await Bus.Send<AddProductToPercentDiscountCommand, AddProductToPercentDiscountCommandResponse>(
                   command);
            return Ok(response);
        }

        public async Task<IHttpActionResult> Delete(Guid percentDiscountId, Guid productId)
        {
            var command = new DeleteProductFromPercentDiscountCommand
            {
                PercentDiscount = percentDiscountId,
                ProductId = productId

            };
            var response =
                await Bus.Send<DeleteProductFromPercentDiscountCommand, DeleteProductFromPercentDiscountCommandResponse>(
                    command);
            return Ok(response);
        }
        public async Task<IHttpActionResult> Get(Guid percentDiscountId)
        {
            var result = await _discountQueryService.GetProductPercentDiscountById(percentDiscountId);
            return Ok(result);
        }
    }
}