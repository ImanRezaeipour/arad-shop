using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Products.Commands;
using Shopping.Commands.Commands.Products.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Products
{
    public class DeleteProductController:ApiControllerBase
    {
        public DeleteProductController(ICommandBus bus):base(bus)
        {
            
        }

        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };
            var response = await Bus.Send<DeleteProductCommand, DeleteProductCommandResponse>(command);
            return Ok(response);
        }
    }
}