using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Infrastructure.Asp.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var isNallable=true;
                foreach (var item in actionContext.ModelState.Keys)
                {
                    if (item.Contains("Nullable"))
                    {
                        isNallable = false;
                       
                    }
                }
                if (isNallable)
                {
                    var responseModel = new ResponseModel
                    {
                        Success = false,
                        ResponseData = null,
                        Message = actionContext.ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage
                    };
                    actionContext.Response =
                        actionContext
                            .Request.CreateResponse(HttpStatusCode.OK, responseModel);
                }
            }
        }
    }
}
