using System.Linq;
using Shopping.QueryModel.Implements.Complaints;

namespace Shopping.QueryService.Interfaces.Complaints
{
    public interface IComplaintQueryService
    {
        IQueryable<ComplaintDto> GetAll();
    }
}