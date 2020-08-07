using System;
using System.Linq;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Kernel.Notify.Message.Implements.Installer;
using Shopping.AsyncCommanBus.Implements;
using Shopping.AsyncCommanBus.Installer;
using Shopping.CommandHandler.CommandHandlers.Brands;
using Shopping.DomainModel.Aggregates.Brands.Services;
using Shopping.DomainModel.Aggregates.Products.Services;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.DomainEvent;
using Shopping.QueryService.Implements.Brands;
using Shopping.Repository.Read;
using Shopping.Repository.Read.Context;
using Shopping.Repository.Read.Interface;
using Shopping.Repository.Write;
using Shopping.Repository.Write.Context;
using Shopping.Repository.Write.Interface;

namespace Shopping.CastleWindsor.Configs
{
    public class ShoppingConfigInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(TransactionalCommandHandlerDecorator<,>))
                .LifestyleScoped());
            container.Install(new CommandBusInstaller());
            container.Register(Classes.FromAssemblyContaining<BrandCommandHandler>().Pick().WithServiceAllInterfaces()
                .LifestyleScoped());
            container.Register(Classes.FromAssemblyContaining<BrandDomainService>().Pick().WithServiceAllInterfaces()
                .LifestyleScoped());
            container.Install(new NotifyInstaller());

            var autoMapperProfiles = typeof(BrandProfile).Assembly.GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Profile)))
                .Select(Activator.CreateInstance)
                .OfType<Profile>()
                .ToList();
            Mapper.Initialize(cfg => 
            {
                autoMapperProfiles.ForEach(cfg.AddProfile);
            });


            container.Register(Classes.FromAssemblyContaining<BrandQueryService>().Pick().WithServiceAllInterfaces()
                .LifestyleScoped());

            container.Register(Classes.FromAssemblyContaining<ProductDomainService>().Pick().WithServiceAllInterfaces()
                .LifestyleScoped());
            container.Register(Component.For<IDomainEventHandlerFactory>().ImplementedBy<DomainEventHandlerFactory>().LifestyleScoped());

            //register repository components
            container.Register(Component.For<IContext>().ImplementedBy<DataContext>()
                .LifestyleScoped());
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>))
                .LifestyleScoped());

            container.Register(Component.For<ISeqRepository>().ImplementedBy<SeqRepository>().LifestyleScoped());

            container.Register(Component.For<ReadOnlyDataContext>().LifestyleScoped());
            container.Register(Component.For(typeof(IReadOnlyRepository<,>)).ImplementedBy(typeof(ReadOnlyRepository<,>))
                .LifestyleScoped());
            DataContext.ExecuteMigration();
            ReadOnlyDataContext.ExecuteMigration();
            DomainEventServiceLocator.SetContainer(container);
        }
    }
}