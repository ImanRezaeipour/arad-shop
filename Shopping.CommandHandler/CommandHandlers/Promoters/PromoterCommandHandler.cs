using System;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Promoters.Commands;
using Shopping.Commands.Commands.Promoters.Responses;
using Shopping.DomainModel.Aggregates.Promoters.Aggregates;
using Shopping.DomainModel.Aggregates.Promoters.Interfaces;
using Shopping.Infrastructure;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Promoters
{
    public class PromoterCommandHandler :
        ICommandHandler<CreatePromoterCommand, PromoterCommandResponse>
    , ICommandHandler<UpdatePromoterCommand, PromoterCommandResponse>
    , ICommandHandler<DeletePromoterCommand, PromoterCommandResponse>
    {
        private readonly IRepository<Promoter> _repository;
        private readonly ISeqRepository _seqRepository;
        private readonly IPromoterDomainService _promoterDomainService;
        public PromoterCommandHandler(IRepository<Promoter> repository, IPromoterDomainService promoterDomainService, ISeqRepository seqRepository)
        {
            _repository = repository;
            _promoterDomainService = promoterDomainService;
            _seqRepository = seqRepository;
        }
        public async Task<PromoterCommandResponse> Handle(CreatePromoterCommand command)
        {
            await _promoterDomainService.CheckPromoterIsExist(command.NationalCode);
            var code = _seqRepository.GetNextSequenceValue(SqNames.PromoterSequence);
            var promoter = new Promoter(Guid.NewGuid(), code, command.FirstName, command.LastName, command.NationalCode,
                command.MobileNumber);
            _repository.Add(promoter);
            return new PromoterCommandResponse();
        }

        public async Task<PromoterCommandResponse> Handle(UpdatePromoterCommand command)
        {
            var promoter = await _repository.FindAsync(command.Id);
            if (promoter == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            await _promoterDomainService.CheckPromoterIsExistForUpdate(promoter.Id, command.NationalCode);
            promoter.NationalCode = command.NationalCode;
            promoter.LastName = command.LastName;
            promoter.FirstName = command.FirstName;
            promoter.MobileNumber = command.MobileNumber;
            return new PromoterCommandResponse();
        }
        public async Task<PromoterCommandResponse> Handle(DeletePromoterCommand command)
        {
            var promoter = await _repository.FindAsync(command.Id);
            if (promoter == null)
            {
                throw new DomainException("شخص یافت نشد");
            }
            promoter.CustomerSubsets.Clear();
            _repository.Remove(promoter);
            return new PromoterCommandResponse();
        }
    }
}