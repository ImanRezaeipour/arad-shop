using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Persons.Commands.Customer;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.DomainModel.Aggregates.BaseEntities.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Entities;
using Shopping.DomainModel.Aggregates.Persons.Entities.CustomerEntities;
using Shopping.DomainModel.Aggregates.Persons.Interfaces;
using Shopping.DomainModel.Aggregates.Persons.ValueObjects;
using Shopping.Infrastructure;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Helper;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;
using Shopping.Shared.Events.Interfaces.Users;
using Shopping.Shared.Events.Messages.Users;

namespace Shopping.CommandHandler.CommandHandlers.Persons
{
    public class CustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
        , ICommandHandler<DeActiveCustomerCommand, DeActiveCustomerCommandResponse>
        , ICommandHandler<ActiveCustomerCommand, ActiveCustomerCommandResponse>
        , ICommandHandler<AddCustomerAddressCommand, AddCustomerAddressCommandResponse>
        , ICommandHandler<DeleteCustomerAddressCommand, DeleteCustomerAddressCommandResponse>
        , ICommandHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>
    {
        private readonly IBus _eventBus;
        private readonly IRepository<City> _cityRepository;
        private readonly ISeqRepository _seqRepository;
        private readonly IRepository<Customer> _repository;
        private readonly IPersonDomainService _personDomainService;
        private readonly IContext _context;
        public CustomerCommandHandler(
            IRepository<City> cityRepository,
            IRepository<Customer> repository,
            IPersonDomainService personDomainService,
            ISeqRepository seqRepository,
            IBus eventBus,
            IContext context)
        {
            _cityRepository = cityRepository;
            _repository = repository;
            _personDomainService = personDomainService;
            _seqRepository = seqRepository;
            _eventBus = eventBus;
            _context = context;
        }

        public Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommand command)
        {
            var customerNumber = _seqRepository.GetNextSequenceValue(SqNames.CustomerNumberSequence);
            _personDomainService.CheckCustomerIsExist(command.UserId);
            var customer = new Customer(Guid.NewGuid(), command.FirstName, command.LastName, command.EmailAddress,
                command.UserId, DefultCustomerAddress.CreateNull(), command.MobileNumber, customerNumber, command.BirthDate)
            {
                CustomerAddresses = new List<CustomerAddress>(),
                AppInfos = new List<AppInfo>()
            };
            _repository.Add(customer);
            _personDomainService.SetCustomerRecommender(command.RecommendCode, customer);
            return Task.FromResult(new CreateCustomerCommandResponse());
        }

        public async Task<AddCustomerAddressCommandResponse> Handle(AddCustomerAddressCommand command)
        {
            var customer = await _repository.AsQuery().SingleOrDefaultAsync(p => p.UserId == command.UserId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }

            var city = _cityRepository.Find(command.CityId);
            if (city == null)
            {
                throw new DomainException("شهر یافت نشد");
            }

            var address = new CustomerAddress(Guid.NewGuid(), command.Title, command.AddressText, command.PhoneNumber,
                command.CityId, city.Code, city.CityName, command.Position.ToDbGeography(), city.Province.Id,
                city.Province.Name, city.Province.Code, false);
            if (!customer.CustomerAddresses.Any())
            {
                customer.DefultCustomerAddress = new DefultCustomerAddress(address.Id, command.Title, command.AddressText,
                    command.PhoneNumber, command.CityId, city.Code, city.CityName, command.Position.ToDbGeography(),
                    city.Province.Id, city.Province.Name, city.Province.Code);
            }
            customer.CustomerAddresses.Add(address);
            return new AddCustomerAddressCommandResponse();
        }

        public async Task<DeleteCustomerAddressCommandResponse> Handle(DeleteCustomerAddressCommand command)
        {
            var customer = await _repository.AsQuery().SingleOrDefaultAsync(p => p.UserId == command.UserId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            _personDomainService.AddressIsDefaultAddress(customer, command.CustomerAddressId);
            var address = customer.CustomerAddresses.SingleOrDefault(p => p.Id == command.CustomerAddressId);
            if (address == null)
            {
                throw new DomainException("ادرس یافت نشد");
            }
            customer.CustomerAddresses.Remove(address);
            return new DeleteCustomerAddressCommandResponse();
        }

        public async Task<ActiveCustomerCommandResponse> Handle(ActiveCustomerCommand command)
        {
            var customer = await _repository.FindAsync(command.Id);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            customer.Active();
            _context.SaveChanges();
            await _eventBus.Publish<IActiveUserEvent>(new ActiveUserEvent(customer.UserId, AppType.Customer));
            return new ActiveCustomerCommandResponse();
        }

        public async Task<DeActiveCustomerCommandResponse> Handle(DeActiveCustomerCommand command)
        {
            var customer = await _repository.FindAsync(command.Id);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            customer.DeActive();
            _context.SaveChanges();
            await _eventBus.Publish<IDeActiveUserEvent>(new DeActiveUserEvent(customer.UserId, AppType.Customer));
            return new DeActiveCustomerCommandResponse();
        }

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommand command)
        {
            var customer = await _repository.FindAsync(command.CustomerId);
            if (customer == null)
            {
                throw new DomainException("مشتری یافت نشد");
            }
            customer.FirstName = command.FirstName;
            customer.LastName = command.LastName;
            customer.EmailAddress = command.EmailAddress;
            customer.BirthDate = command.BirthDate;

            foreach (var customerAddressItem in command.CustomerAddresses)
            {
                var customerAddress = customer.CustomerAddresses.SingleOrDefault(p => p.Id == customerAddressItem.Id);
                if (customerAddress == null)
                {
                    throw new DomainException("آدرس مشتری یافت نشد");
                }
                customerAddress.AddressText = customerAddressItem.AddressText;
                customerAddress.PhoneNumber = customerAddressItem.PhoneNumber;
                customerAddress.Geography = customerAddressItem.Position.ToDbGeography();
                customerAddress.IsDefault = customerAddressItem.IsDefault;
                customerAddress.Title = customerAddressItem.Title;
                var city = _cityRepository.Find(customerAddressItem.CityId);
                if (city == null)
                {
                    throw new DomainException("شهر یافت نشد");
                }
                customerAddress.CityName = city.CityName;
                customerAddress.CityCode = city.Code;
                customerAddress.CityId = city.Id;
                customerAddress.ProvinceId = city.Province.Id;
                customerAddress.ProvinceCode = city.Province.Code;
                customerAddress.ProvinceName = city.Province.Name;
                if (customerAddressItem.IsDefault)
                {
                    customer.DefultCustomerAddress = new DefultCustomerAddress(customerAddress.Id, customerAddress.Title,
                        customerAddress.AddressText, customerAddress.PhoneNumber, customerAddress.CityId,
                        customerAddress.CityCode, customerAddress.CityName, customerAddress.Geography,
                        customerAddress.ProvinceId, customerAddress.ProvinceName, customerAddress.ProvinceCode);
                }
            }
            return new UpdateCustomerCommandResponse();
        }
    }
}