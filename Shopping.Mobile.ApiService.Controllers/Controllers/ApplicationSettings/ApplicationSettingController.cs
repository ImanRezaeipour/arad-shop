using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.ApplicationSettings;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.ApplicationSettings
{
    public class ApplicationSettingController : ApiMobileControllerBase
    {
        private readonly IApplicationSettingQueryService _applicationSettingQueryService;
        public ApplicationSettingController(IApplicationSettingQueryService applicationSettingQueryService)
        {
            _applicationSettingQueryService = applicationSettingQueryService;
        }

        /// <summary>
        /// دریافت تنظیمات پایه سیستم در صورت وجود
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _applicationSettingQueryService.GetApplicationSetting()
            };
            return Ok(dto);
        }
    }
}