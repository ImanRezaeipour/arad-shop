using System.Web.Http;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;

namespace Shopping.Mobile.ApiService
{
    public class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var webApiServer = new HttpServer(config);
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.MapODataServiceRoute("odata", null, GetEdmModel(),
                new DefaultODataBatchHandler(webApiServer));
            config.EnsureInitialized();
            config.EnableDependencyInjection();
        }
        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}