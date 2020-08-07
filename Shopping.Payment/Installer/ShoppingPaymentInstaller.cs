using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kernel.Notify.Message.Implements.Installer;
using Shopping.DomainModel.Aggregates.Brands.Services;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write;
using Shopping.Repository.Write.Context;
using Shopping.Repository.Write.Interface;

namespace Shopping.Payment.Installer
{
    public class ShoppingPaymentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestylePerWebRequest());

            //register repository components
            container.Register(Component.For<IContext>().ImplementedBy<DataContext>()
                .LifestylePerWebRequest());
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>))
                .LifestylePerWebRequest());
            container.Register(Classes.FromAssemblyContaining<BrandDomainService>().Pick().WithServiceAllInterfaces()
                .LifestylePerWebRequest());
            DataContext.ExecuteMigration();
            container.Install(new NotifyInstaller());
        }
    }
}