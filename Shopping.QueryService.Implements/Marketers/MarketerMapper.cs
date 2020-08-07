using AutoMapper;
using Shopping.DomainModel.Aggregates.Marketers.Aggregates;
using Shopping.DomainModel.Aggregates.Marketers.Entities;
using Shopping.QueryModel.QueryModels.Marketers;

namespace Shopping.QueryService.Implements.Marketers
{
    public static class MarketerMapper
    {
        public static IMarketerDto ToDto(this Marketer src)
        {
            return Mapper.Map<IMarketerDto>(src);
        }
        public static IMarketerFullInfoDto ToFullInfoDto(this Marketer src)
        {
            return Mapper.Map<IMarketerFullInfoDto>(src);
        }
        public static IMarketerCommentDto ToDto(this MarketerComment src)
        {
            return Mapper.Map<IMarketerCommentDto>(src);
        }
    }
}