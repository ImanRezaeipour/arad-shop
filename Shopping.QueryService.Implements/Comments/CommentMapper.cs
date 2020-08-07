using AutoMapper;
using Shopping.DomainModel.Aggregates.Comments.Aggregates;
using Shopping.QueryModel.QueryModels.Comments;

namespace Shopping.QueryService.Implements.Comments
{
    public static class CommentMapper
    {
        public static ICommentDto ToDto(this Comment src)
        {
            return Mapper.Map<ICommentDto>(src);
        }
    }
}