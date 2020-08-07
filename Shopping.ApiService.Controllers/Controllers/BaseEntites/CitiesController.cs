using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.BaseEntities.Commands;
using Shopping.Commands.Commands.BaseEntities.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.BaseEntities;

namespace Shopping.ApiService.Controllers.Controllers.BaseEntites
{
    [RoutePrefix("api/Cities")]
    public class CitiesController : ApiControllerBase
    {
        private readonly ICityQueryService _cityQueryService;
        public CitiesController(ICityQueryService cityQueryService, ICommandBus bus) : base(bus)
        {
            _cityQueryService = cityQueryService;
        }
        [Authorize(Roles = "Support,Admin,Operator")]
        public IHttpActionResult Get()
        {
            return Ok(_cityQueryService.GetAll());
        }
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_cityQueryService.GetById(id));
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Post(CreateCityCommand command)
        {
            var response = await
                Bus.Send<CreateCityCommand, CreateCityCommandResponse>(command);
            return Ok(response);
        }
        [Route("Update")]
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(UpdateCityCommand command)
        {
            var response = await
                Bus.Send<UpdateCityCommand, UpdateCityCommandResponse>(command);
            return Ok(response);
        }
        [Route("DeActive")]
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(DeActiveCityCommand command)
        {
            var response = await
                Bus.Send<DeActiveCityCommand, DeActiveCityCommandResponse>(command);
            return Ok(response);
        }
        [Route("Active")]
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(ActiveCityCommand command)
        {
            var response = await
                Bus.Send<ActiveCityCommand, ActiveCityCommandResponse>(command);
            return Ok(response);
        }
    }
}