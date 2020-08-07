using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shopping.AsyncCommanBus.Handling;
using Shopping.AsyncCommanBus.Implements;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.AsyncCommanBus.Installer
{
    /// <summary>
    /// Bus installer
    /// </summary>
    public class CommandBusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICommandBus>().ImplementedBy<Bus>()
                .LifestyleSingleton());

            container.Register(Component.For<ICommandHandlerFactory>().Instance(new CommandHandlerFactory(container))
                .LifestyleSingleton());
        }
    }
}