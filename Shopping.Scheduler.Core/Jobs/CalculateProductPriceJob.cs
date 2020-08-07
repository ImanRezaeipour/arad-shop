using System;
using System.Web.Hosting;
using FluentScheduler;
using NLog;
using Shopping.Scheduler.Core.AppStart;
using Shopping.Scheduler.Core.Interfaces;

namespace Shopping.Scheduler.Core.Jobs
{
    public class CalculateProductPriceJob : IJob, IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private readonly ICalculateProductPriceService _calculateProductPriceService;
        private readonly Logger _logger;
        public CalculateProductPriceJob()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _calculateProductPriceService = Bootstrapper.Container.Resolve<ICalculateProductPriceService>();
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
                    _logger.Info($"CalculateProductPriceJob execute at ### {DateTime.Now} ###");

                    _calculateProductPriceService.Calculate();

                    _logger.Info($"CalculateProductPriceJob end at ### {DateTime.Now} ###");
                }
            }
            finally
            {
                lock (_lock)
                {
                    Bootstrapper.Container.Release(_calculateProductPriceService);
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