using System;

namespace Shopping.QueryModel.QueryModels.Comments
{
    public interface ICommentDto
    {
        Guid Id { get; set; }
        DateTime CreationTime { get; set; }
        int Degree { get; set; }
        int ItemDegree { get; set; }
        long FactorId { get;  set; }
    }
}