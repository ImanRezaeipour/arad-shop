using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class ShopController : ApiControllerBase
    {
        private readonly IShopQueryService _personQueryService;
        public ShopController(ICommandBus bus, IShopQueryService personQueryService) : base(bus)
        {
            _personQueryService = personQueryService;
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_personQueryService.GetAll());
        }
        [Authorize(Roles = "Support,Admin,Operator")]
        public IHttpActionResult Get(Guid shopId)
        {
            return Ok(_personQueryService.GetById(shopId));
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(UpdateShopCommand command)
        {
            var response = await Bus.Send<UpdateShopCommand, UpdateShopCommandResponse>(command);
            return Ok(response);

        }
    }
}