using System;
using System.Web.Hosting;
using FluentScheduler;
using NLog;
using Shopping.Scheduler.Core.AppStart;
using Shopping.Scheduler.Core.Interfaces;

namespace Shopping.Scheduler.Core.Jobs
{
    public class ShopAccountingJob : IJob, IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private readonly IRegisterPersonAccountingService _registerPersonAccountingService;
        private readonly Logger _logger;
        public ShopAccountingJob()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _registerPersonAccountingService = Bootstrapper.Container.Resolve<IRegisterPersonAccountingService>();
            HostingEnvironment.RegisterObject(this);
        }
        public void Execute()
        {
            try
            {
                lock (_lock)
                {
                    if (_shuttingDown)
                        return;
                    _logger.Info($"ShopAccountingJob execute at ### {DateTime.Now} ###");

                    _registerPersonAccountingService.RegisterShops();

                    _logger.Info($"ShopAccountingJob end at ### {DateTime.Now} ###");
                }
            }
            finally
            {
                lock (_lock)
                {
                    Bootstrapper.Container.Release(_registerPersonAccountingService);
                }
                HostingEnvironment.UnregisterObject(this);
            }
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }
    }
}