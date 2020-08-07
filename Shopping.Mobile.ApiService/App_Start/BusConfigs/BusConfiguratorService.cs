using System;
using Castle.MicroKernel.Registration;
using MassTransit;
using Shopping.Config.IOC;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.BusConfigs
{
    public class BusConfiguratorService
    {
        public static void Configure()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://" + ApiSettings.Default.RabbitMQ_ServerName + "/"), h =>
                {
                    h.Username(ApiSettings.Default.RabbitMQ_UserName);
                    h.Password(ApiSettings.Default.RabbitMQ_Password);
                });
            });
            busControl.Start();
            IocManager.Instance.IocContainer.Register(Component.For<IBus>().Instance(busControl).LifestyleSingleton());
        }
    }
}