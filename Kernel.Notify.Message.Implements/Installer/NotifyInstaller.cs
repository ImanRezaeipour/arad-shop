using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kernel.Notify.Message.Interfaces;

namespace Kernel.Notify.Message.Implements.Installer
{
    public class NotifyInstaller : IWindsorInstaller
    {
        /// <summary>
        ///   Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFcmNotification>().ImplementedBy<FcmNotification>().LifestyleSingleton());
        }
    }
}