using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Shop
{
    public class ShopPersonController : ApiMobileControllerBase
    {
        public ShopPersonController(ICommandBus bus) : base(bus)
        {

        }

        public async Task<IHttpActionResult> Post(CreateShopCommand command)
        {
            //command.UserId = UserId;
            var commandResponse = await Bus.Send<CreateShopCommand, CreateShopPersonCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت نام با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}