using Castle.Windsor;

namespace Shopping.Infrastructure.Core.DomainEvent
{
    public class DomainEventServiceLocator
    {
        private static IWindsorContainer _maintContainer;
        public static void SetContainer(IWindsorContainer container)
        {
            _maintContainer = container;
        }
        public static T GetInstance<T>()
        {
            return _maintContainer.Resolve<T>();
        }
        public static T[] GetAllInstances<T>()
        {
            return _maintContainer.ResolveAll<T>();
        }
    }
}