using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.OrdersSuggestions.Commands;
using Shopping.Commands.Commands.OrdersSuggestions.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.OrdersSuggestions
{
    public class RejectOrderSuggestionController:ApiMobileControllerBase
    {
        public RejectOrderSuggestionController(ICommandBus bus):base(bus)
        {
            
        }
        [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Put(RejectOrderSuggestionCommand command)

        {
            var commandResponse = await Bus.Send<RejectOrderSuggestionCommand, RejectOrderSuggestionCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "رد پیشنهاد با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
    }
}