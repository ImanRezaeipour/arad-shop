using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.MarketerSalaryPayments.Commands;
using Shopping.Commands.Commands.MarketerSalaryPayments.Responses;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.Aggregates;
using Shopping.DomainModel.Aggregates.MarketerSalaryPayments.ValueObjects;
using Shopping.DomainModel.Aggregates.Shared;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.MarketerSalaryPayments
{
    public class MarketerSalaryPaymentCommandHandler : ICommandHandler<CreateMarketerSalaryPaymentCommand, CreateMarketerSalaryPaymentCommandResponse>
        , ICommandHandler<UpdateMarketerSalaryPaymentCommand, UpdateMarketerSalaryPaymentCommandResponse>
    {
        private readonly IRepository<MarketerSalaryPayment> _repository;
        private readonly IRepository<Marketer> _marketeRepository;
        public MarketerSalaryPaymentCommandHandler(IRepository<MarketerSalaryPayment> repository, IRepository<Marketer> marketeRepository)
        {
            _repository = repository;
            _marketeRepository = marketeRepository;
        }

        public async Task<CreateMarketerSalaryPaymentCommandResponse> Handle(CreateMarketerSalaryPaymentCommand command)
        {
            var marketer = await _marketeRepository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.MarketerId);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            var userInfo = new UserInfo(command.UserInfoCommand.UserId, command.UserInfoCommand.FirstName,
                command.UserInfoCommand.LastName);
            var periodSalary = new PeriodSalary(command.PeriodSalaryCommand.FromDate, command.PeriodSalaryCommand.ToDate);
            var marketerSalaryPayment =
                new MarketerSalaryPayment(Guid.NewGuid(), command.Amount, marketer, periodSalary, userInfo);
            _repository.Add(marketerSalaryPayment);
            return new CreateMarketerSalaryPaymentCommandResponse();
        }

        public async Task<UpdateMarketerSalaryPaymentCommandResponse> Handle(UpdateMarketerSalaryPaymentCommand command)
        {
            var marketerSalaryPayment = await _repository.AsQuery().SingleOrDefaultAsync(p => p.Id == command.Id);
            if (marketerSalaryPayment == null)
            {
                throw new DomainException("پرداخت بازاریاب یافت نشد");
            }
            var marketer = _marketeRepository.AsQuery().SingleOrDefault(p => p.Id == command.MarketerId);
            if (marketer == null)
            {
                throw new DomainException("بازاریاب یافت نشد");
            }
            marketerSalaryPayment.Marketer = marketer;
            marketerSalaryPayment.Amount = command.Amount;
            var userInfo = new UserInfo(command.UserInfoCommand.UserId, command.UserInfoCommand.FirstName,
                command.UserInfoCommand.LastName);
            marketerSalaryPayment.UserInfo = userInfo;
            var periodSalary = new PeriodSalary(command.PeriodSalaryCommand.FromDate, command.PeriodSalaryCommand.ToDate);
            marketerSalaryPayment.PeriodSalary = periodSalary;
            return new UpdateMarketerSalaryPaymentCommandResponse();
        }
    }
}