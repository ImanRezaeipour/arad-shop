using System;
using System.Web.Hosting;
using FluentScheduler;
using NLog;
using Shopping.Scheduler.Core.AppStart;
using Shopping.Scheduler.Core.Interfaces;

namespace Shopping.Scheduler.Core.Jobs
{
    public class TestJob: IJob, IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private readonly IShopCustomerSubsetSettlementService _shopCustomerSubsetSettlementService;
        private readonly Logger _logger;
        public TestJob()
        {
            _logger = LogManager.GetCurrentClassLogger();
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
                    _logger.Info($"test execute at ### {DateTime.Now} ###");
                }
            }
            finally
            {
                lock (_lock)
                {
                    Bootstrapper.Container.Release(_shopCustomerSubsetSettlementService);
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