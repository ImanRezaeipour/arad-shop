using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.MarketerSalaryPayments.Commands;
using Shopping.Commands.Commands.MarketerSalaryPayments.Responses;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.MarketerSalaryPayments;

namespace Shopping.ApiService.Controllers.Controllers.MarketerSalaryPayments
{
    [Authorize(Roles = "Support,Admin")]
    public class MarketerSalaryPaymentController : ApiControllerBase
    {
        private readonly IMarketerSalaryPaymentQueryService _marketerSalaryPaymentQueryService;

        public MarketerSalaryPaymentController(ICommandBus bus,
            IMarketerSalaryPaymentQueryService marketerSalaryPaymentQueryService) : base(bus)
        {
            _marketerSalaryPaymentQueryService = marketerSalaryPaymentQueryService;
        }

        public async Task<IHttpActionResult> Post(CreateMarketerSalaryPaymentCommand command)
        {
            var userInfoCommand = new UserInfoCommandItem(UserId, FirstName, LastName);
            command.UserInfoCommand = userInfoCommand;
            var response =
                await Bus.Send<CreateMarketerSalaryPaymentCommand, CreateMarketerSalaryPaymentCommandResponse>(command);
            return Ok(response);
        }

        public async Task<IHttpActionResult> Put(UpdateMarketerSalaryPaymentCommand command)
        {
            var userInfoCommand = new UserInfoCommandItem(UserId, FirstName, LastName);
            command.UserInfoCommand = userInfoCommand;
            var response =
                await Bus.Send<UpdateMarketerSalaryPaymentCommand, UpdateMarketerSalaryPaymentCommandResponse>(command);
            return Ok(response);
        }
        [CustomQueryable]
        public IHttpActionResult Get()
        {
            return Ok(_marketerSalaryPaymentQueryService.GetAll());
        }

        public IHttpActionResult Get(Guid id)
        {
            return Ok(_marketerSalaryPaymentQueryService.GetById(id));
        }
    }
}