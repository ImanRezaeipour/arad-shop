using System;
using System.Linq;
using System.Threading.Tasks;
using Shopping.AsyncCommanBus.Handling;
using Shopping.Commands.Commands.Complaints.Commands;
using Shopping.Commands.Commands.Complaints.Responses;
using Shopping.DomainModel.Aggregates.Complaints.Aggregates;
using Shopping.DomainModel.Aggregates.Persons.Aggregates;
using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Interface;

namespace Shopping.CommandHandler.CommandHandlers.Complaints
{
    public class ComplaintCommandHandler:
        ICommandHandler<CreateRegisterComplaintCommand, CreateRegisterComplaintCommandResponse>
    {
        private readonly IRepository<Shop> _shopRepository;
        private readonly IRepository<Complaint> _repository;
        public ComplaintCommandHandler(IRepository<Shop> shopRepository, IRepository<Complaint> repository)
        {
            _shopRepository = shopRepository;
            _repository = repository;
        }


        public Task<CreateRegisterComplaintCommandResponse> Handle(CreateRegisterComplaintCommand command)
        {
            var shop = _shopRepository.AsQuery().SingleOrDefault(p => p.Id == command.ShopId);
            if (shop == null)
            {
                throw new DomainException("فروشگاه یافت نشد");
            }
            var complaint = new Complaint(Guid.NewGuid(), command.FirstName, command.LastName, command.Email,
                command.Title, command.Description, shop);
            _repository.Add(complaint);
            return Task.FromResult(new CreateRegisterComplaintCommandResponse());
        }
       
    }
}