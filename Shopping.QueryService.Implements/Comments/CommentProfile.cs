using AutoMapper;
using Shopping.DomainModel.Aggregates.Comments.Aggregates;
using Shopping.QueryModel.QueryModels.Comments;

namespace Shopping.QueryService.Implements.Comments
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, ICommentDto>();
        }
    }
}