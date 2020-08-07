using System;
using System.Web.Hosting;
using FluentScheduler;
using NLog;
using Shopping.Scheduler.Core.AppStart;
using Shopping.Scheduler.Core.Interfaces;

namespace Shopping.Scheduler.Core.Jobs
{
    public class FactorAccountingJob: IJob, IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private readonly IRegisterFactorAccountingService _registerFactorAccountingService;
        private readonly Logger _logger;
        public FactorAccountingJob()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _registerFactorAccountingService = Bootstrapper.Container.Resolve<IRegisterFactorAccountingService>();
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
                    _logger.Info($"FactorAccountingJob execute at ### {DateTime.Now} ###");

                    _registerFactorAccountingService.RegisterFactors();

                    _logger.Info($"FactorAccountingJob end at ### {DateTime.Now} ###");
                }
            }
            finally
            {
                lock (_lock)
                {
                    Bootstrapper.Container.Release(_registerFactorAccountingService);
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