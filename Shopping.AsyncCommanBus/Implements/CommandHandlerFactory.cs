using Castle.Windsor;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Infrastructure.Core.CommandBus.Messages;

namespace Shopping.AsyncCommanBus.Implements
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IWindsorContainer _container;
        public CommandHandlerFactory(IWindsorContainer container)
        {
            _container = container;
        }
        public ICommandHandler<TCommandRequest, TCommandResponse> GetHandlerFor<TCommandRequest, TCommandResponse>()
            where TCommandRequest : ICommand
            where TCommandResponse : ICommandResponse
        {
            return _container.Resolve<TransactionalCommandHandlerDecorator<TCommandRequest, TCommandResponse>>();
        }

        public ICommandHandler<TCommandRequest> GetHandlerFor<TCommandRequest>() where TCommandRequest : ICommand
        {
            return _container.Resolve<ICommandHandler<TCommandRequest>>();
        }
    }
}