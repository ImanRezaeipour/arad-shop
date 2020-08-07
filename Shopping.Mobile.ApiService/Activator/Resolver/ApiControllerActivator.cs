using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.Windsor;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace Shopping.Mobile.ApiService.Activator.Resolver
{
    public class ApiDependencyResolver : IDependencyResolver
    {
        public ApiDependencyResolver(IWindsorContainer container)
        {
            Container = container;
        }

        public IWindsorContainer Container
        {
            get;
            private set;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Kernel.HasComponent(serviceType) ? Container.Resolve(serviceType) : null;
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
                return Enumerable.Empty<object>();
            }
        }
        public IDependencyScope BeginScope()
        {
            return new SharedDependencyResolver(Container);
        }
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}