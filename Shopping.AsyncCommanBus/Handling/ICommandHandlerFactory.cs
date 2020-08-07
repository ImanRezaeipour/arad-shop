using Shopping.Infrastructure.Core.CommandBus.Messages;

namespace Shopping.AsyncCommanBus.Handling
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommandRequest, TCommandResponse> GetHandlerFor<TCommandRequest, TCommandResponse>()
            where TCommandRequest : ICommand
            where TCommandResponse : ICommandResponse;

        ICommandHandler<TCommandRequest> GetHandlerFor<TCommandRequest>()
            where TCommandRequest : ICommand;
    }
}