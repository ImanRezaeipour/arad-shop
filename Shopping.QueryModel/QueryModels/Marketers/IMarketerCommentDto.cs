using System;

namespace Shopping.QueryModel.QueryModels.Marketers
{
    public interface IMarketerCommentDto
    {
        Guid Id { get; set; }
        string Comment { get; set; }
    }
}