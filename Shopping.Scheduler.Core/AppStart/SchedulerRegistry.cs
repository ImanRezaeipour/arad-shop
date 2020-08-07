using FluentScheduler;
using NLog;
using Shopping.Scheduler.Core.Jobs;

namespace Shopping.Scheduler.Core.AppStart
{
    public class SchedulerRegistry : Registry
    {
        public SchedulerRegistry()
        {
            var logger = LogManager.GetCurrentClassLogger();
            JobManager.JobException += info => logger.Log(LogLevel.Error, info.Exception);

            //Schedules
            //Schedule(() => new CalculateProductPriceJob())
            //    .NonReentrant().ToRunEvery(1).Days().At(00, 05);

            //Schedule(() => new FactorAccountingJob())
            //    .NonReentrant().ToRunEvery(1).Days().At(00, 30);

            //Schedule(() => new ShopAccountingJob())
            //    .NonReentrant().ToRunNow()
            //    .AndEvery(5).Minutes();

            //Schedule(() => new CustomerAccountingJob())
            //    .NonReentrant().ToRunNow()
            //    .AndEvery(5).Minutes();

            //Schedule(() => new ShopCustomerSubsetSettlementPaidFactorJob())
            //    .NonReentrant().ToRunEvery(1).Days().At(23, 45);

            //Schedule(() => new ShopCustomerSubsetSettlementPaidFactorAccountingJob())
            //    .NonReentrant().ToRunEvery(1).Days().At(23, 59);


            Schedule(() => new CheckPrivateOrderExpireJob())
                .NonReentrant().ToRunNow().AndEvery(1).Seconds();
        }
    }
}