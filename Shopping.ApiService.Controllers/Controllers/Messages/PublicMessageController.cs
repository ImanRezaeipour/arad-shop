using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Messages.Commands;
using Shopping.Commands.Commands.Messages.Responses;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Messages;

namespace Shopping.ApiService.Controllers.Controllers.Messages
{
    public class PublicMessageController : ApiControllerBase
    {
        private readonly IMessageQueryService _messageQueryService;
        public PublicMessageController(ICommandBus bus, IMessageQueryService messageQueryService) : base(bus)
        {
            _messageQueryService = messageQueryService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Post(CreatePublicMessageCommand command)
        {
            command.UserInfo = new UserInfoCommandItem(UserId, FirstName, LastName);
            var response = await Bus.Send<CreatePublicMessageCommand, CreatePublicMessageCommandResponse>(command);
            return Ok(response);
        }
        [CustomQueryable]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_messageQueryService.GetAllPublicMessage());
        }
    }
}