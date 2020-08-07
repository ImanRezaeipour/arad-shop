using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Shared;
using Shopping.Commands.Commands.ShopAcceptors.Commands;
using Shopping.Commands.Commands.ShopAcceptors.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.ShopAcceptors;

namespace Shopping.ApiService.Controllers.Controllers.ShopAcceptors
{
    [RoutePrefix("api/ShopAcceptor")]
    [Authorize(Roles = "Support,Admin")]
    public class ShopAcceptorController:ApiControllerBase
    {
        private readonly IShopAcceptorQueryService _acceptorQueryService;
        public ShopAcceptorController(ICommandBus bus, IShopAcceptorQueryService acceptorQueryService):base(bus)
        {
            _acceptorQueryService = acceptorQueryService;
        }
        public async Task<IHttpActionResult> Post(CreateShopAcceptorCommand command)
        {
            command.UserInfo = new UserInfoCommandItem(UserId , FirstName , LastName);
            var response =
                await Bus.Send<CreateShopAcceptorCommand, CreateShopAcceptorCommandResponse>(command);
            return Ok(response);
        }
        [Route("Reject")]
        public async Task<IHttpActionResult> Put(RejectShopAcceptorCommand command)
        {
            var response =
                await Bus.Send<RejectShopAcceptorCommand, RejectShopAcceptorCommandResponse>(command);
            return Ok(response);
        }
        [Route("Accept")]
        public async Task<IHttpActionResult> Put(AcceptShopAcceptorCommand command)
        {
            var response =
                await Bus.Send<AcceptShopAcceptorCommand, AcceptShopAcceptorCommandResponse>(command);
            return Ok(response);
        }
        [CustomQueryable]
        public IHttpActionResult Get()
        {
            return Ok(_acceptorQueryService.GetAll());
        }

        public IHttpActionResult Get(Guid id)
        {
            return Ok(_acceptorQueryService.GetById(id));
        }
    }
}