using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace Shopping.ApiService.Activator.Resolver
{
    public class SharedDependencyResolver : IDependencyScope
    {
        public SharedDependencyResolver(IWindsorContainer container)
        {
            Container = container;
            Scope = Container.BeginScope();
        }

        public IWindsorContainer Container
        {
            get;
            private set;
        }

        public IDisposable Scope
        {
            get;
            private set;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Kernel.HasComponent(serviceType) ?
                    Container.Resolve(serviceType) : null;
            }
            catch (ComponentNotFoundException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType).Cast<object>();
            }
            catch (ComponentNotFoundException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}