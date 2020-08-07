using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.FakeIos.Customers.Commands;
using Shopping.Commands.Commands.FakeIos.Customers.Responses;
using Shopping.Commands.Commands.FakeIos.Orders.Commands;
using Shopping.Commands.Commands.FakeIos.Orders.Responses;
using Shopping.DomainModel.Aggregates.FakeIos.Customers;
using Shopping.DomainModel.Aggregates.FakeIos.Orders;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.FakeIos
{
    public class FakeIosCommandHandler : ICommandHandler<RegisterFakeCustomerIosCommand, RegisterFakeCustomerIosCommandResponse>
        , ICommandHandler<LoginFakeCustomerIosCommand, LoginFakeCustomerIosCommandResponse>
        , ICommandHandler<CreateFakeOrderIosCommand, FakeOrderIosCommandResponse>
        , ICommandHandler<ChangeFakeOrderIosCommand, FakeOrderIosCommandResponse>
    {
        private readonly IRepository<FakeCustomerIos> _customerRepository;
        private readonly IRepository<FakeOrderIos> _orderRepository;
        public FakeIosCommandHandler(IRepository<FakeCustomerIos> customerRepository, IRepository<FakeOrderIos> orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        public async Task<RegisterFakeCustomerIosCommandResponse> Handle(RegisterFakeCustomerIosCommand command)
        {
            if (await _customerRepository.AsQuery().AnyAsync(p => p.Username == command.Username))
            {
                throw new DomainException("username is exist");
            }
            var fakeCustomerIos =
                new FakeCustomerIos(Guid.NewGuid(), command.FullName, command.Username, command.Password);
            _customerRepository.Add(fakeCustomerIos);
            return new RegisterFakeCustomerIosCommandResponse(fakeCustomerIos.Id);
        }
        public async Task<LoginFakeCustomerIosCommandResponse> Handle(LoginFakeCustomerIosCommand command)
        {
            var user = await _customerRepository.AsQuery()
                .FirstOrDefaultAsync(p => p.Username == command.Username && p.Password == command.Password);
            if (user == null)
            {
                throw new DomainException("username or password is incorrect");
            }
            return new LoginFakeCustomerIosCommandResponse(user.Id, user.FullName);
        }
        public Task<FakeOrderIosCommandResponse> Handle(CreateFakeOrderIosCommand command)
        {
            var order = new FakeOrderIos(Guid.NewGuid(), command.FullName, command.CustomerId, command.AddressText,
                command.AddressLat, command.AddressLng,
                command.Items.Select(item => new FakeOrderIosItem(Guid.NewGuid(), item.ProductId, item.Name, item.Image,
                    item.Brand, item.Quantity)).ToList());
            _orderRepository.Add(order);
            return Task.FromResult(new FakeOrderIosCommandResponse());
        }

        public async Task<FakeOrderIosCommandResponse> Handle(ChangeFakeOrderIosCommand command)
        {
            var order = await _orderRepository.FindAsync(command.OrderId);
            order.State = command.State;
            return new FakeOrderIosCommandResponse();
        }
    }
}