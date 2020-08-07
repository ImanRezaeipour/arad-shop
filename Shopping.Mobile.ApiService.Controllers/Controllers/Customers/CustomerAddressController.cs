using System;
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
    public class CustomerAddressController : ApiMobileControllerBase
    {
        private readonly ICustomerQueryService _customerQueryService;
        private readonly IShopQueryService _shopQueryService;
        public CustomerAddressController(ICommandBus bus, ICustomerQueryService customerQueryService, IShopQueryService shopQueryService) : base(bus)
        {
            _customerQueryService = customerQueryService;
            _shopQueryService = shopQueryService;
        }
        [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Post(AddCustomerAddressCommand command)
        {
            command.UserId = UserId;
            var commandResponse = await Bus.Send<AddCustomerAddressCommand, AddCustomerAddressCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "اضافه کردن آدرس با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Authorize(Roles = "Customer")]
        public async Task<IHttpActionResult> Delete(Guid customerAddressId)
        {
            DeleteCustomerAddressCommand command = new DeleteCustomerAddressCommand
            {
                UserId = UserId,
                CustomerAddressId = customerAddressId
            };
            var commandResponse = await Bus.Send<DeleteCustomerAddressCommand, DeleteCustomerAddressCommandResponse>(command);
            var response = new ResponseModel
            {
                Message = "حذف آدرس با موفقیت انجام شد",
                Success = true,
                ResponseData = commandResponse
            };
            return Ok(response);
        }
        [Authorize(Roles = "Customer")]
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _customerQueryService.GetCustomerAddressesByUserId(UserId)
            };
            return Ok(dto);
        }
        [Authorize(Roles = "Customer")]
        [Route("api/CustomerAddress/ShopsInArea")]
        public IHttpActionResult GetShopsInArea(Guid customerAddressId)
        {
            var dto = new MobileResultDto
            {
                Result = _shopQueryService.GetShopsInCustomerAddressArea(UserId, customerAddressId)
            };
            return Ok(dto);
        }
    }
}