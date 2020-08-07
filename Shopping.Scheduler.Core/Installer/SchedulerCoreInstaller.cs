using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shopping.DomainModel.Aggregates.Discounts.Interfaces;
using Shopping.DomainModel.Aggregates.Discounts.Services;
using Shopping.Scheduler.Core.AppStart;
using Shopping.Scheduler.Core.Interfaces;
using Shopping.Scheduler.Core.Services;

namespace Shopping.Scheduler.Core.Installer
{
    public class SchedulerCoreInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<SchedulerServiceControl>());
            container.Register(Component.For<ICalculateProductPriceService>()
                .ImplementedBy<CalculateProductPriceService>().LifestyleTransient());
            container.Register(Component.For<IRegisterFactorAccountingService>()
                .ImplementedBy<RegisterFactorAccountingService>().LifestyleTransient());
            container.Register(Component.For<IRegisterPersonAccountingService>()
                .ImplementedBy<RegisterPersonAccountingService>().LifestyleTransient());

            container.Register(Component.For<IShopCustomerSubsetSettlementService>()
                .ImplementedBy<ShopCustomerSubsetSettlementService>().LifestyleTransient());

            container.Register(Component.For<ICheckPrivateOrderExpireService>()
                .ImplementedBy<CheckPrivateOrderExpireService>().LifestyleTransient());


        }
    }
}