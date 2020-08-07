using System;
using Castle.Windsor;

namespace Shopping.Config.IOC
{
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        IWindsorContainer IocContainer { get; }
    }
}