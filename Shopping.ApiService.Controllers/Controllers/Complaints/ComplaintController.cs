using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Complaints.Commands;
using Shopping.Commands.Commands.Complaints.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Complaints;

namespace Shopping.ApiService.Controllers.Controllers.Complaints
{
    public class ComplaintController:ApiControllerBase
    {
        private readonly IComplaintQueryService _complaintQueryService;
        public ComplaintController(ICommandBus bus, IComplaintQueryService complaintQueryService):base(bus)
        {
            _complaintQueryService = complaintQueryService;
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Post(CreateRegisterComplaintCommand command)
        {
            var response = await Bus.Send<CreateRegisterComplaintCommand, CreateRegisterComplaintCommandResponse>(command);
            return Ok(response);
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_complaintQueryService.GetAll());
        }
    }
}