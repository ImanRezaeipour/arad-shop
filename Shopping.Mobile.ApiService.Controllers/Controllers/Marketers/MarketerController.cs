using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Marketers;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Marketers
{
    public class MarketerController : ApiMobileControllerBase
    {
        private readonly IMarketerQueryService _marketerQueryService;
        public MarketerController(IMarketerQueryService marketerQueryService)
        {
            _marketerQueryService = marketerQueryService;
        }
        public async Task<IHttpActionResult> Get(Guid barcodeId)
        {
            var dto = new MobileResultDto
            {
                Result = await _marketerQueryService.GetMarketerByBarcodeId(barcodeId)
            };
            return Ok(dto);
        }
    }
}