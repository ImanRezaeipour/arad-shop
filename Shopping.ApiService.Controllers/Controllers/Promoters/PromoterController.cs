using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Promoters.Commands;
using Shopping.Commands.Commands.Promoters.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Promoters;

namespace Shopping.ApiService.Controllers.Controllers.Promoters
{
    [Authorize(Roles = "Support,Admin")]
    public class PromoterController : ApiControllerBase
    {
        private readonly IPromoterQueryService _promoterQueryService;
        public PromoterController(ICommandBus bus, IPromoterQueryService promoterQueryService) : base(bus)
        {
            _promoterQueryService = promoterQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get()
        {
            return Ok(_promoterQueryService.GetAll());
        }
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await _promoterQueryService.GetAsync(id);
            return Ok(result);
        }
        public async Task<IHttpActionResult> Post(CreatePromoterCommand command)
      {
            var response = await Bus.Send<CreatePromoterCommand, PromoterCommandResponse>(command);
            return Ok(response);
        }
        public async Task<IHttpActionResult> Put(UpdatePromoterCommand command)
        {
            var response = await Bus.Send<UpdatePromoterCommand, PromoterCommandResponse>(command);
            return Ok(response);
        }
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var command = new DeletePromoterCommand
            {
                Id = id
            };
            var response = await Bus.Send<DeletePromoterCommand, PromoterCommandResponse>(command);
            return Ok(response);
        }
    }
}