using System.Threading.Tasks;
using Shopping.Infrastructure.Core.CommandBus.Messages;

namespace Shopping.AsyncCommanBus.Handling
{
    public interface ICommandHandler
    { }
    public interface ICommandHandler<TCommandRequest, TCommandResponse> : ICommandHandler
        where TCommandRequest : ICommand
        where TCommandResponse : ICommandResponse
    {
        Task<TCommandResponse> Handle(TCommandRequest command);
    }
    public interface ICommandHandler<TCommandRequest> : ICommandHandler
        where TCommandRequest : ICommand
    {
        Task Handle(TCommandRequest command);
    }
}