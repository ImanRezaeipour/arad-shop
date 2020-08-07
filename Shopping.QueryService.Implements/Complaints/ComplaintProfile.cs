using AutoMapper;
using Shopping.DomainModel.Aggregates.Complaints.Aggregates;
using Shopping.QueryModel.Implements.Complaints;
using Shopping.QueryModel.QueryModels.Complaints;

namespace Shopping.QueryService.Implements.Complaints
{
    public class ComplaintProfile:Profile
    {
        public ComplaintProfile()
        {
            CreateMap<Complaint, IComplaintDto>();
            CreateMap<Complaint, ComplaintDto>();
        }
    }
}