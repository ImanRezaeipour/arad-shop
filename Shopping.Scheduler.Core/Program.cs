using NLog;
using Shopping.Scheduler.Core.AppStart;
using Topshelf;
using Topshelf.CastleWindsor;

namespace Shopping.Scheduler.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Start();
            HostFactory.Run(hc =>
            {
                hc.UseNLog();
                hc.UseWindsorContainer(Bootstrapper.Container);
                hc.Service<SchedulerServiceControl>(s =>
                {
                    s.ConstructUsingWindsorContainer();
                    s.WhenStarted((tc, hostControl) => tc.Start(hostControl));
                    s.WhenStopped((tc, hostControl) => tc.Stop(hostControl));
                });
                hc.OnException(ex =>
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Error(ex);
                });
#if DEBUG
                hc.RunAsLocalSystem();
#else
                hc.RunAsNetworkService();
#endif 
                hc.SetDescription("Shopping Scheduler Core");
                hc.SetDisplayName("Shopping Scheduler Core");
                hc.SetServiceName("ShoppingSchedulerCore");

                hc.EnableServiceRecovery(rc =>
                {
                    rc.OnCrashOnly();
                    rc.RestartService(0);
                    rc.RestartService(1);
                    rc.RestartService(5);
                    rc.SetResetPeriod(1);
                });
            });
        }
    }
}
