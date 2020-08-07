using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Customer;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Persons;

#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Customers
{
    public class CustomerController : ApiMobileControllerBase
    {
        private readonly ICustomerQueryService _customerQueryService;
        public CustomerController(ICommandBus bus, ICustomerQueryService customerQueryService) : base(bus)
        {
            _customerQueryService = customerQueryService;
        }
        [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Post(CreateCustomerCommand command)
        {
            command.UserId = UserId;
            command.MobileNumber = MobileNumber;
            var commandResponse = await Bus.Send<CreateCustomerCommand, CreateCustomerCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "ثبت نام مشتری با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Authorize(Roles = "Customer")]
        public IHttpActionResult Get()
        {
            return Ok(_customerQueryService.GetCustomerByUserId(UserId));
        }
    }
}