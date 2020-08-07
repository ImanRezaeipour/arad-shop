using Castle.Windsor;
using Shopping.Scheduler.Core.Installer;

namespace Shopping.Scheduler.Core.AppStart
{
    public static class Bootstrapper
    {
        public static IWindsorContainer Container;
        static Bootstrapper()
        {
            Container = new WindsorContainer();
        }
        public static void Start()
        {
            Container.Install(new SchedulerCoreInstaller());
        }
        public static void Stop()
        {
            Container?.Dispose();
        }
    }
}