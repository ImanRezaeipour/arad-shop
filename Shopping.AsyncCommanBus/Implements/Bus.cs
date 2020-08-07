using System;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.Core.CommandBus.Messages;

namespace Shopping.AsyncCommanBus.Implements
{
    public class Bus : ICommandBus
    {
        private readonly ICommandHandlerFactory _commandExecutorFactory;

        public Bus(ICommandHandlerFactory commandExecuteFactory)
        {
            _commandExecutorFactory = commandExecuteFactory;
        }
        public Task<TCommandResponse> Send<TCommandRequest, TCommandResponse>(TCommandRequest command)
            where TCommandRequest : ICommand
            where TCommandResponse : ICommandResponse
        {
            return GetCommandHandler<TCommandRequest, TCommandResponse>().Handle(command);
        }

        public Task Send<TCommandRequest>(TCommandRequest command) where TCommandRequest : ICommand
        {
            try
            {
                return GetCommandHandler<TCommandRequest>().Handle(command);
            }
            catch (Exception ex)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        private ICommandHandler<TCommandRequest, TCommandResponse> GetCommandHandler<TCommandRequest, TCommandResponse>()
            where TCommandRequest : ICommand
            where TCommandResponse : ICommandResponse
        {
            var executer =
                _commandExecutorFactory.GetHandlerFor<TCommandRequest, TCommandResponse>();
            if (executer == null)
            {
                throw new InvalidOperationException("No command executor registered for command type " +
                                                    typeof(TCommandRequest).FullName);
            }
            return executer;
        }
        private ICommandHandler<TCommandRequest> GetCommandHandler<TCommandRequest>()
            where TCommandRequest : ICommand
        {
            var executer =
                _commandExecutorFactory.GetHandlerFor<TCommandRequest>();
            if (executer == null)
            {
                throw new InvalidOperationException("No command executor registered for command type " +
                                                    typeof(TCommandRequest).FullName);
            }
            return executer;
        }
    }
}