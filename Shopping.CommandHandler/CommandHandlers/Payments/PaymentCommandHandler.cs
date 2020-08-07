using System;
using System.Linq;
using System.Threading.Tasks;
using Parbad.Core;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Payments.Commands;
using Shopping.Commands.Commands.Payments.Responses;
using Shopping.DomainModel.Aggregates.ApplicationSettings.Aggregates;
using Shopping.DomainModel.Aggregates.Factors.Aggregates;
using Shopping.DomainModel.Aggregates.Payments.Aggregates;
using Shopping.DomainModel.Aggregates.Payments.ValueObjects;
using Shopping.Infrastructure;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Payments
{
    public class PaymentCommandHandler : ICommandHandler<PaymentFactorCommand, PaymentFactorCommandResponse>
    {
        private readonly IRepository<Payment> _repository;
        private readonly IRepository<ApplicationSetting> _settingRepository;
        private readonly IContext _context;
        private readonly IRepository<Factor> _factorRepository;
        public PaymentCommandHandler(IRepository<Payment> repository,
            IRepository<Factor> factorRepository,
            IContext context,
            IRepository<ApplicationSetting> settingRepository)
        {
            _repository = repository;
            _factorRepository = factorRepository;
            _context = context;
            _settingRepository = settingRepository;
        }
        public async Task<PaymentFactorCommandResponse> Handle(PaymentFactorCommand command)
        {
            var appSetting = _settingRepository.AsQuery().FirstOrDefault();
            var factor = await _factorRepository.FindAsync(command.FactorId);
            if (factor == null)
            {
                throw new DomainException("فاکتور یافت نشد");
            }
            var expireTime = DateTime.Now.AddMinutes(-appSetting.FactorExpireTime);
            if (factor.CreationTime <= expireTime)
            {
                throw new DomainException("زمان پرداخت به پایان رسیده است");
            }
            var payment = new Payment(command.Gateway, Convert.ToInt64(decimal.Floor(factor.SystemDiscountPrice)), factor.Id)
            {
                //for complex type
                PaymentResponse = new PaymentResponse(null, null, null, VerifyResultStatus.NotValid)
            };
            payment = _repository.Add(payment);
            _context.SaveChanges();
            return new PaymentFactorCommandResponse(GetIpgUrl(payment.Id));
        }
        private string GetIpgUrl(long paymentId)
        {
            var result = $"{ShoppingConfiguration.IpgUrl}?paymentId={paymentId}&callbackUrl={ShoppingConfiguration.IpgCallBackUrl}?paymentIdentity={paymentId}";
            return result;
        }
    }
}