using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.ProductsSuggestions.Commands.CustomerProductSuggestion;
using Shopping.Commands.Commands.ProductsSuggestions.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.ProductsSuggestion;

#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.ProductsSuggestions
{
    public class CustomerProductSuggestionController : ApiMobileControllerBase
    {
        private readonly ICustomerProductsSuggestionQueryService _customerProductsSuggestionQueryService;
        public CustomerProductSuggestionController(ICommandBus bus, ICustomerProductsSuggestionQueryService customerProductsSuggestionQueryService) : base(bus)
        {
            _customerProductsSuggestionQueryService = customerProductsSuggestionQueryService;
        }
        [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Post(CreateCustomerProductSuggestionCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<CreateCustomerProductSuggestionCommand, CreateCustomerProductSuggestionCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت کالای پیشنهادی با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        /// <summary>
        /// دریافت لیست محصولات پیشنهادی یک کاربر
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Customer")]
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_customerProductsSuggestionQueryService.GetByUserId(UserId, pagedInput));
        }
    }
}