using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Persons;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Shops
{
    public class ShopController : ApiMobileControllerBase
    {
        private readonly IShopQueryService _shopQueryService;
        public ShopController(ICommandBus bus, IShopQueryService shopQueryService) : base(bus)
        {
            _shopQueryService = shopQueryService;
        }
        [Authorize(Roles = "Shop")]
        public async Task<IHttpActionResult> Post(CreateShopCommand command)
        {
            command.UserId = UserId;
            command.MobileNumber = MobileNumber;
            var commandResponse = await Bus.Send<CreateShopCommand, CreateShopCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت نام با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Authorize(Roles = "Shop")]
        public IHttpActionResult Get()
        {
            return Ok(_shopQueryService.GetByUserId(UserId));
        }
    }
}