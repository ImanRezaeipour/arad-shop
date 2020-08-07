using AutoMapper;
using Shopping.DomainModel.Aggregates.Messages.Aggregates;
using Shopping.QueryModel.QueryModels.Messages;

namespace Shopping.QueryService.Implements.Messages
{
    public static class MessageMapper
    {
        public static IPrivateMessageDto ToDto(this PrivateMeassge src)
        {
            return Mapper.Map<IPrivateMessageDto>(src);
        }

        public static IPublicMessageDto ToDto(this PublicMessage src)
        {
            return Mapper.Map<IPublicMessageDto>(src);
        }
    }
}