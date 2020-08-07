using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.ProductsSuggestions.Commands;
using Shopping.Commands.Commands.ProductsSuggestions.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.ProductsSuggestions
{
    public class RejectProductSuggestionController:ApiControllerBase
    {
        public RejectProductSuggestionController(ICommandBus bus):base(bus)
        {
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(RejectProductSuggestionCommand command)
        {
            var response = await Bus.Send<RejectProductSuggestionCommand, RejectProductSuggestionCommandResponse>(command);
            return Ok(response);
        }
    }
}