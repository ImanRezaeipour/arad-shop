using System;
using System.Data.Entity.SqlServer;
using System.Web.Mvc;
using System.Web.Routing;
using Parbad.Configurations;
using Parbad.Providers.Parbad;
using Shopping.Payment.Installer;

namespace Shopping.Payment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            SqlProviderServices.SqlServerTypesAssemblyName =
                "Microsoft.SqlServer.Types, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //ipg test config parbad
            ParbadConfiguration.Gateways.ConfigureParbadVirtualGateway(
                new ParbadVirtualGatewayConfiguration("Parbad.axd"));

            ControllerBuilder.Current
                .SetControllerFactory(typeof(WindsorControllerFactory));
        }
    }
}
