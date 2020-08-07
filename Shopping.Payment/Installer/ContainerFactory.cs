using Castle.Windsor;

namespace Shopping.Payment.Installer
{
    public class ContainerFactory
    {
        private static IWindsorContainer _container;
        private static readonly object SyncObject = new object();
        public static IWindsorContainer Current()
        {
            if (_container == null)
            {
                lock (SyncObject)
                {
                    if (_container == null)
                    {
                        _container = new WindsorContainer();
                        _container.Install(new ShoppingPaymentInstaller());
                    }
                }
            }
            return _container;
        }
    }
}