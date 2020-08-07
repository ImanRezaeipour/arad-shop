using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Brands.Commands;
using Shopping.Commands.Commands.Brands.Responses;
using Shopping.Infrastructure.OData;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.Brands;

namespace Shopping.ApiService.Controllers.Controllers.Brands
{
    public class BrandController : ApiControllerBase
    {
        private readonly IBrandQueryService _brandQueryService;
        public BrandController(ICommandBus bus, IBrandQueryService brandQueryService) : base(bus)
        {
            _brandQueryService = brandQueryService;
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_brandQueryService.GetAllQueryable());
        }
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_brandQueryService.GetBrandById(id));
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Post(CreateBrandCommand command)
        {
            var response = await
                Bus.Send<CreateBrandCommand, CreateBrandCommandResponse>(command);
            return Ok(response);
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(UpdateBrandCommand command)
        {
            var response = await
                Bus.Send<UpdateBrandCommand, UpdateBrandCommandResponse>(command);
            return Ok(response);
        }

        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var command = new DeleteBrandCommand
            {
                Id = id
            };
            var response = await Bus.Send<DeleteBrandCommand, DeleteBrandCommandResponse>(command);
            return Ok(response);

        }

    }
}