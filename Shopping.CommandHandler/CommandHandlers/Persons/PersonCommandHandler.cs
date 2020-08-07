using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Persons.Commands;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates.Abstract;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;
using Shopping.Shared.Enums;

namespace Shopping.CommandHandler.CommandHandlers.Persons
{
    public class PersonCommandHandler
        : ICommandHandler<SetAppInfoCommand, AppInfoCommandResponse>
    , ICommandHandler<MuteAppCommand, AppInfoCommandResponse>
    , ICommandHandler<UnMuteAppCommand, AppInfoCommandResponse>
    {
        private readonly IRepository<Person> _repository;
        public PersonCommandHandler(IRepository<Person> repository)
        {
            _repository = repository;
        }
        public async Task<AppInfoCommandResponse> Handle(SetAppInfoCommand command)
        {
            switch (command.AppType)
            {
                case AppType.Customer:
                    var customer = await _repository.AsQuery().OfType<Customer>()
                        .SingleOrDefaultAsync(p => p.UserId == command.UserId);
                    if (customer == null)
                    {
                        throw new DomainException("مشتری یافت نشد");
                    }
                    customer.SetAppInfo(command.OsType, command.PushToken, command.AuthDeviceId);
                    break;
                case AppType.Shop:
                    var shop = await _repository.AsQuery().OfType<Shop>()
                        .SingleOrDefaultAsync(p => p.UserId == command.UserId);
                    if (shop == null)
                    {
                        throw new DomainException("فروشگاه یافت نشد");
                    }
                    shop.SetAppInfo(command.OsType, command.PushToken, command.AuthDeviceId);
                    break;
                default:
                    throw new DomainException("پارامتر ارسالی نامعتبر می باشد");
            }
            return new AppInfoCommandResponse();
        }

        public async Task<AppInfoCommandResponse> Handle(MuteAppCommand command)
        {
            switch (command.AppType)
            {
                case AppType.Customer:
                    var customer = await _repository.AsQuery().OfType<Customer>()
                        .SingleOrDefaultAsync(p => p.UserId == command.UserId);
                    if (customer == null)
                    {
                        throw new DomainException("مشتری یافت نشد");
                    }
                    foreach (var customerAppInfo in customer.AppInfos)
                    {
                        customerAppInfo.MuteApp();
                    }
                    break;
                case AppType.Shop:
                    var shop = await _repository.AsQuery().OfType<Shop>()
                        .SingleOrDefaultAsync(p => p.UserId == command.UserId);
                    if (shop == null)
                    {
                        throw new DomainException("فروشگاه یافت نشد");
                    }
                    foreach (var shopAppInfo in shop.AppInfos)
                    {
                        shopAppInfo.MuteApp();
                    }
                    break;
                default:
                    throw new DomainException("پارامتر ارسالی نامعتبر می باشد");
            }
            return new AppInfoCommandResponse();
        }

        public async Task<AppInfoCommandResponse> Handle(UnMuteAppCommand command)
        {
            switch (command.AppType)
            {
                case AppType.Customer:
                    var customer = await _repository.AsQuery().OfType<Customer>()
                        .SingleOrDefaultAsync(p => p.UserId == command.UserId);
                    if (customer == null)
                    {
                        throw new DomainException("مشتری یافت نشد");
                    }
                    foreach (var customerAppInfo in customer.AppInfos)
                    {
                        customerAppInfo.UnMuteApp();
                    }
                    break;
                case AppType.Shop:
                    var shop = await _repository.AsQuery().OfType<Shop>()
                        .SingleOrDefaultAsync(p => p.UserId == command.UserId);
                    if (shop == null)
                    {
                        throw new DomainException("فروشگاه یافت نشد");
                    }
                    foreach (var shopAppInfo in shop.AppInfos)
                    {
                        shopAppInfo.UnMuteApp();
                    }
                    break;
                default:
                    throw new DomainException("پارامتر ارسالی نامعتبر می باشد");
            }
            return new AppInfoCommandResponse();
        }
    }
}