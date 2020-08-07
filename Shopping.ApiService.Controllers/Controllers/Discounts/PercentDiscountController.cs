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
    public class PercentDiscountController : ApiControllerBase
    {
        private readonly IPercentDiscountQueryService _discountQueryService;
        public PercentDiscountController(ICommandBus bus, IPercentDiscountQueryService discountQueryService) : base(bus)
        {
            _discountQueryService = discountQueryService;
        }
        public async Task<IHttpActionResult> Post(CreateDiscountPercentCommand command)
        {
            command.UserInfoCommand = new UserInfoCommandItem(UserId, FirstName, LastName);
            var response = await Bus.Send<CreateDiscountPercentCommand, CreateDiscoutPercentCommandResponse>(command);
            return Ok(response);
        }
        public async Task<IHttpActionResult> Put(UpdateDiscountPercentCommand command)
        {
            command.UserInfoCommand = new UserInfoCommandItem(UserId, FirstName, LastName);
            var response = await Bus.Send<UpdateDiscountPercentCommand, UpdateDiscountPercentCommandResponse>(command);
            return Ok(response);
        }
        public IHttpActionResult Get(Guid percentDiscountId)
        {
            return Ok(_discountQueryService.GetById(percentDiscountId));
        }
    }
}