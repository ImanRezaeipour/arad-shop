
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Products.Commands;
using Shopping.Commands.Commands.Products.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Products;

namespace Shopping.ApiService.Controllers.Controllers.Products
{
	public class ProductController : ApiControllerBase
	{
		private readonly IProductQueryService _productQueryService;
		public ProductController(ICommandBus bus, IProductQueryService productQueryService) : base(bus)
		{
			_productQueryService = productQueryService;
		}
        /// <summary>
        /// نماش تمامی محصولات
        /// </summary>
        /// <returns></returns>
        [CustomQueryable]
        //[Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid? brandId=null, Guid? categoryRootId=null, Guid? subCategoryId=null)
        {
			return Ok(_productQueryService.GetAll(brandId, categoryRootId, subCategoryId));
		}
		/// <summary>
		/// نمایش تکی یک محصول
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        [Authorize(Roles = "Support,Admin")]
		public IHttpActionResult Get(Guid id)
		{
		    var result = _productQueryService.GetById(id);
		    result.Price *= 10; 
            return Ok(result);
		}
		/// <summary>
		/// ثبت یک محصول
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
        [Authorize(Roles = "Support,Admin")]
		public async Task<IHttpActionResult> Post(CreateProductCommand command)
		{
			var response = await Bus.Send<CreateProductCommand, CreateProductCommandResponse>(command);
			return Ok(response);
		}
		/// <summary>
		/// ویرایش یک محصول
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
        [Authorize(Roles = "Support,Admin")]
		public async Task<IHttpActionResult> Put(UpdateProductCommand command)
		{
			var response = await Bus.Send<UpdateProductCommand, UpdateProductCommandResponse>(command);
			return Ok(response);
		}

	    ///// <summary>
	    ///// نماش تمامی محصولات با فیلتر
	    ///// </summary>
	    ///// <returns></returns>
	    //[CustomQueryable]
	    //[Authorize(Roles = "Support,Admin")]
	    //[Route("Filter")]
     //   public IHttpActionResult Get(Guid? brandId, Guid? categoryRootId, Guid? subCategoryId)
	    //{
	    //    return Ok(_productQueryService.GetAllByFilter(brandId,categoryRootId,subCategoryId));
	    //}
    }
}