using AutoMapper;
using Shopping.DomainModel.Aggregates.Complaints.Aggregates;
using Shopping.QueryModel.QueryModels.Complaints;

namespace Shopping.QueryService.Implements.Complaints
{
    public static class ComplaintMapper
    {
        public static IComplaintDto ToDto(this Complaint src)
        {
            return Mapper.Map<IComplaintDto>(src);
        }
    }
}