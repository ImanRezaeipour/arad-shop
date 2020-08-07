using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.ApplicationSettings.Commands;
using Shopping.Commands.Commands.ApplicationSettings.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.ApplicationSettings;

namespace Shopping.ApiService.Controllers.Controllers.ApplicationSettings
{
    public class ApplicationSettingController : ApiControllerBase
    {
        private readonly IApplicationSettingQueryService _applicationSettingQueryService;
        public ApplicationSettingController(ICommandBus bus, IApplicationSettingQueryService applicationSettingQueryService) : base(bus)
        {
            _applicationSettingQueryService = applicationSettingQueryService;
        }
        /// <summary>
        /// بروز رسانی تنظیمات پایه
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Post(UpdateApplicationSettingCommand command)
        {
            var response = await Bus.Send<UpdateApplicationSettingCommand, UpdateApplicationSettingCommandResponse>(command);
            return Ok(response);
        }
        /// <summary>
        /// دریافت تنظیمات پایه سیستم در صورت وجود
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_applicationSettingQueryService.GetApplicationSetting());
        }
    }
}