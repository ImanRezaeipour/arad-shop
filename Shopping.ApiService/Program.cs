using System;
using NLog;
using Topshelf;

namespace Shopping.ApiService
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            try
            {
                var host = HostFactory.New(x =>
                {
                    x.UseNLog();
                    x.Service<ApiStart>(s =>
                    {
                        s.ConstructUsing(name => new ApiStart());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.OnException(ex =>
                    {
                        var logger = LogManager.GetCurrentClassLogger();
                        logger.Error(ex);
                    });
                    x.EnableShutdown();
#if (DEBUG)
                    x.RunAsLocalService();
#endif
#if (!DEBUG)
                x.RunAsNetworkService();
#endif
                    x.StartAutomaticallyDelayed();
                    x.SetDescription("Shopping Panel API");
                    x.SetDisplayName("Shopping Panel API");
                    x.SetServiceName("ShoppingPanelAPI");
                    x.EnableServiceRecovery(rc =>
                    {
                        rc.OnCrashOnly();
                        rc.RestartService(0);
                        rc.RestartService(1);
                        rc.RestartService(5);
                        rc.SetResetPeriod(1);
                    });
                });

                host.Run();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
