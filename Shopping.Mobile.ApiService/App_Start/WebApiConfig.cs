using System.Web.Http;
using Shopping.Infrastructure.Asp.Filters;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new MobileExceptionFilterAttribute());
            config.Filters.Add(new ValidateModelStateFilter());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
