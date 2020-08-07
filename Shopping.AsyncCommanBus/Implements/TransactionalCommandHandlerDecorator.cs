using System;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.CommandBus.Messages;

namespace Shopping.AsyncCommanBus.Implements
{
    public class TransactionalCommandHandlerDecorator<TCommandRequest, TCommandResponse>
        : ICommandHandler<TCommandRequest, TCommandResponse> where TCommandResponse : ICommandResponse where TCommandRequest : ICommand
    {
        private readonly ICommandHandler<TCommandRequest, TCommandResponse> _handler;
        private readonly IContext _context;
        public TransactionalCommandHandlerDecorator(ICommandHandler<TCommandRequest, TCommandResponse> handler, IContext context)
        {
            _handler = handler;
            _context = context;
        }
        public async Task<TCommandResponse> Handle(TCommandRequest command)
        {
            try
            {
                var commandResponse =await _handler.Handle(command);
                _context.SaveChanges();
                return commandResponse;
            }
            catch (Exception e)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw ;
            }
        }
    }
}
