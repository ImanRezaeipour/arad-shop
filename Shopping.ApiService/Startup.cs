using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Web.Http;
using System.Web.Http.Cors;
using FluentValidation.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.OAuth;
using Owin;
using OWIN.Windsor.DependencyResolverScopeMiddleware;
using Shopping.ApiService;
using Shopping.ApiService.Activator.Resolver;
using Shopping.ApiService.BusConfigs;
using Shopping.CastleWindsor.Configs;
using Shopping.Config.IOC;
using Shopping.Infrastructure.Asp.Extentions;
using Shopping.Repository.Read.FullTextSerch;
using Shopping.SignalR.Hubs;

[assembly: OwinStartup(typeof(Startup))]
namespace Shopping.ApiService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            SqlProviderServices.SqlServerTypesAssemblyName =
                "Microsoft.SqlServer.Types, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";

            IocManager.Instance.IocContainer.Install(new ShoppingConfigInstaller());
            IocManager.Instance.IocContainer.Install(new PanelApiServiceInstaller());

            var config = new HttpConfiguration
            {
                DependencyResolver = new ApiDependencyResolver(IocManager.Instance.IocContainer)
            };

            var cors = new EnableCorsAttribute(ApiSettings.Default.Origin, "*", "*");
            config.EnableCors(cors);

            WebApiConfig.Register(config);
            ODataConfig.Register(config);
            ConfigureOAuth(app);
            BusConfiguratorService.Configure();
            config.UseJsonFormatters();
            config.EnsureInitialized();

            app.UseWindsorDependencyResolverScope(config, IocManager.Instance.IocContainer);

            //signalR configuration
            AppDomain.CurrentDomain.Load(typeof(SignalRSender).Assembly.FullName);
            
            app.UseCors(CorsOptions.AllowAll);
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = true
            };
            app.MapSignalR(hubConfiguration);
            app.UseWebApi(config);
            FluentValidationModelValidatorProvider.Configure(config);
            config.FilterConfigRegister();
            DbInterception.Add(new FtsInterceptor());
        }
        public static void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AccessTokenFormat = new TicketDataFormat(MachineKeyDataProtectorHelper.New())
            };
            app.UseOAuthBearerAuthentication(oAuthServerOptions);
        }
    }
}
