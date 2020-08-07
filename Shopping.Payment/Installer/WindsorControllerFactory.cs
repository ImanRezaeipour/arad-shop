using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Shopping.Payment.Installer
{
    /// Use Castle Windsor to create controllers and provide DI
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;
        public WindsorControllerFactory()
        {
            _container = ContainerFactory.Current();
        }
        protected override IController GetControllerInstance(
            RequestContext requestContext,
            Type controllerType)
        {
            return (IController)_container.Resolve(controllerType);
        }
    }
}