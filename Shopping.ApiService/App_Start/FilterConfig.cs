using System.Web.Http;
using Shopping.Infrastructure.Asp.Filters;

namespace Shopping.ApiService
{
    public static class FilterConfig
    {
        public static void FilterConfigRegister(this HttpConfiguration config)
        {
            config.Filters.Add(new CustomExceptionFilterAttribute());
            config.Filters.Add(new ValidateModelStateFilter());
        }
    }
}