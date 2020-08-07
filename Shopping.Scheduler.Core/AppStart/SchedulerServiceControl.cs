using System;
using System.Data.Entity.SqlServer;
using FluentScheduler;
using Topshelf;

namespace Shopping.Scheduler.Core.AppStart
{
    public class SchedulerServiceControl: ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            SqlProviderServices.SqlServerTypesAssemblyName =
                "Microsoft.SqlServer.Types, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91";
            JobManager.Initialize(new SchedulerRegistry());
            return true;
        }
        public bool Stop(HostControl hostControl)
        {
            Bootstrapper.Stop();
            JobManager.StopAndBlock();
            return true;
        }
    }
}