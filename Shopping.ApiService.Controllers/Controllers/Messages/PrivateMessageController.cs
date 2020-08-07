using System;
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
    public class PrivateMessageController : ApiControllerBase
    {
        private readonly IMessageQueryService _messageQueryService;

        public PrivateMessageController(ICommandBus bus, IMessageQueryService messageQueryService)
            : base(bus)
        {
            _messageQueryService = messageQueryService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Post(CreatePrivateMessageCommand command)
        {
            command.UserInfo = new UserInfoCommandItem(UserId, FirstName, LastName);
            var response = await Bus.Send<CreatePrivateMessageCommand, CreatePrivateMessageCommandResponse>(command);
            return Ok(response);
        }
        [CustomQueryable]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Get(Guid personId)
        {
            return Ok(_messageQueryService.GetAllPrivateMessageByPersonId(personId));
        }
    }
}