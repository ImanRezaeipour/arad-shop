using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Products.Commands;
using Shopping.Commands.Commands.Products.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Products
{
	public class ActiveProductController:ApiControllerBase
	{
		public ActiveProductController(ICommandBus bus):base(bus)
		{
		}
		/// <summary>
		/// فعال کردن یک محصول
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
        [Authorize(Roles = "Support,Admin")]
		public async Task<IHttpActionResult> Put(ActiveProductCommand command)
		{
			var response = await Bus.Send<ActiveProductCommand, ActiveProductCommandResponse>(command);
			return Ok(response);
		}
	}
}