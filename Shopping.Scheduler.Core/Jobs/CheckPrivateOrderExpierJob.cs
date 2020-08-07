using System.Web.Hosting;
using FluentScheduler;
using NLog;
using Shopping.Scheduler.Core.AppStart;
using Shopping.Scheduler.Core.Interfaces;

namespace Shopping.Scheduler.Core.Jobs
{
    public class CheckPrivateOrderExpireJob : IJob, IRegisteredObject
    {
        private readonly Logger _logger;
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private readonly ICheckPrivateOrderExpireService _checkPrivateOrderExpireService;
        public CheckPrivateOrderExpireJob()
        {
            _logger = LogManager.GetCurrentClassLogger();
            HostingEnvironment.RegisterObject(this);
            _checkPrivateOrderExpireService = Bootstrapper.Container.Resolve<ICheckPrivateOrderExpireService>();  
        }
        public void Execute()
        {
            try
            {
                lock (_lock)
                {
                    if (_shuttingDown)
                        return;
                    _checkPrivateOrderExpireService.CreateAreaOrder();
                }
            }
            finally
            {
                lock (_lock)
                {
                    Bootstrapper.Container.Release(_checkPrivateOrderExpireService);
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