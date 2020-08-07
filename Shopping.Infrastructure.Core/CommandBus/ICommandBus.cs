using System.Threading.Tasks;
using Shopping.Infrastructure.Core.CommandBus.Messages;

namespace Shopping.Infrastructure.Core.CommandBus
{
    public interface ICommandBus
    {
        Task<TCommandResponse> Send<TCommandRequest, TCommandResponse>(TCommandRequest command)
            where TCommandRequest : ICommand
            where TCommandResponse : ICommandResponse;

        Task Send<TCommandRequest>(TCommandRequest command)
            where TCommandRequest : ICommand;
    }
}
