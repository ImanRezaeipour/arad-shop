using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Shopping.DomainModel.Aggregates.Complaints.Aggregates;
using Shopping.QueryModel.Implements.Complaints;
using Shopping.QueryService.Interfaces.Complaints;
using Shopping.Repository.Read.Interface;

namespace Shopping.QueryService.Implements.Complaints
{
    public class ComplaintQueryService: IComplaintQueryService
    {
        private readonly IReadOnlyRepository<Complaint,Guid> _repository;

        public ComplaintQueryService(IReadOnlyRepository<Complaint, Guid> repository)
        {
            _repository = repository;
        }
        public IQueryable<ComplaintDto> GetAll()
        {
            var result = _repository.AsQuery().ProjectTo<ComplaintDto>();
            return result;
        }
    }
}