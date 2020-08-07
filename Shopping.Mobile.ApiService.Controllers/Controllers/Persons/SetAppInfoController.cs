using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

#pragma warning disable 1591
namespace Shopping.Mobile.ApiService.Controllers.Controllers.Persons
{
    public class SetAppInfoController : ApiMobileControllerBase
    {
        public SetAppInfoController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Shop,Customer")]
        public async Task<IHttpActionResult> Put(SetAppInfoCommand command)
        {
            command.UserId = UserId;
            command.AuthDeviceId = DeviceId;
            var commandResponse = await Bus.Send<SetAppInfoCommand, AppInfoCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت اطلاعات با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}