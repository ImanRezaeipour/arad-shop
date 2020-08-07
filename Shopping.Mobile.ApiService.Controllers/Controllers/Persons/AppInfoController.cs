using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Persons;
using Shopping.Shared.Enums;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Persons
{
    [RoutePrefix("api/AppInfo")]
    [Authorize(Roles = "Support,Admin")]
    public class AppInfoController : ApiMobileControllerBase
    {
        private readonly IPersonQueryService _personQueryService;
        public AppInfoController(ICommandBus bus, IPersonQueryService personQueryService) : base(bus)
        {
            _personQueryService = personQueryService;
        }

        public async Task<IHttpActionResult> Get(AppType appType)
        {
            var result = await _personQueryService.GetAppInfo(UserId, DeviceId, appType);
            return Ok(result);
        }

        [Route("Mute")]
        public async Task<IHttpActionResult> PutMute(MuteAppCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<MuteAppCommand, AppInfoCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "خاموش کردن صدای نوتیفیکیشن با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Route("UnMute")]
        public async Task<IHttpActionResult> PutUnMute(UnMuteAppCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<UnMuteAppCommand, AppInfoCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "روشن کردن صدای نوتیفیکیشن با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}