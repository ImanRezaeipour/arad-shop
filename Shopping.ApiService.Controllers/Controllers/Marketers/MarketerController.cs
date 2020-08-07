using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Marketers.Commands;
using Shopping.Commands.Commands.Marketers.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Marketers;

namespace Shopping.ApiService.Controllers.Controllers.Marketers
{
    [RoutePrefix("api/Marketer")]
    public class MarketerController : ApiControllerBase
    {
        private readonly IMarketerQueryService _marketerQueryService;
        public MarketerController(ICommandBus bus, IMarketerQueryService marketerQueryService) : base(bus)
        {
            _marketerQueryService = marketerQueryService;
        }

    [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Post(CreateMarketerCommand command)
        {
            command.UserId = UserId;
            var response = await Bus.Send<CreateMarketerCommand, MarketerCommandResponse>(command);
            return Ok(response);
        }
    [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(UpdateMarketerCommand command)
        {
            var response = await Bus.Send<UpdateMarketerCommand, UpdateMarketerCommandResponse>(command);
            return Ok(response);
        }

        [CustomQueryable]
    [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_marketerQueryService.GetAll());
        }

    [Authorize(Roles = "Support,Admin,Operator")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await _marketerQueryService.Get(id);
            return Ok(result);
        }

        [Route("DeActive")]
    [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(DeActiveMarketerCommand command)
        {
            var response = await Bus.Send<DeActiveMarketerCommand, MarketerCommandResponse>(command);
            return Ok(response);
        }

        [Route("Active")]
    [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(ActiveMarketerCommand command)
        {
            var response = await Bus.Send<ActiveMarketerCommand, MarketerCommandResponse>(command);
            return Ok(response);
        }

        [Route("Comment")]
    [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Post(AddMarketerCommentCommand command)
        {
            var response = await Bus.Send<AddMarketerCommentCommand, MarketerCommandResponse>(command);
            return Ok(response);
        }

        [Route("Comment")]
    [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> GetComments(long id)
        {
            var result = await _marketerQueryService.GetMarketerComments(id);
            return Ok(result);
        }


    }
}