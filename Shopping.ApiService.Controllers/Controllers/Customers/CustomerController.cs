using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Customer;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Customers
{
    [RoutePrefix("api/Customer")]
    [Authorize(Roles = "Support,Admin")]
    public class CustomerController : ApiControllerBase
    {
        private readonly ICustomerQueryService _customerQueryService;
        public CustomerController(ICommandBus bus, ICustomerQueryService customerQueryService) : base(bus)
        {
            _customerQueryService = customerQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get()
        {
            return Ok(_customerQueryService.GetAll());
        }
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_customerQueryService.Get(id));
        }
        [Route("Address")]
        public async Task<IHttpActionResult> GetAddresses(Guid id)
        {
            var result = await _customerQueryService.GetCustomerAddressesById(id);
            return Ok(result);
        }
        public async Task<IHttpActionResult> Put(UpdateCustomerCommand command)
        {
            var response = await Bus.Send<UpdateCustomerCommand, UpdateCustomerCommandResponse>(command);
            return Ok(response);
        }
    }
}