using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;

namespace Shopping.CastleWindsor.Configs
{
    public class MobileApiServiceInstaller : IWindsorInstaller
    {
        /// <summary>
        ///   Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<ApiMobileControllerBase>().BasedOn<ApiMobileControllerBase>()
                .LifestyleScoped());
        } 
    }
}