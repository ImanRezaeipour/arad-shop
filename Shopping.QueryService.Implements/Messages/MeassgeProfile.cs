using AutoMapper;
using Shopping.DomainModel.Aggregates.Messages.Aggregates;
using Shopping.DomainModel.Aggregates.Messages.Aggregates.Abstract;
using Shopping.QueryModel.QueryModels.Messages;

namespace Shopping.QueryService.Implements.Messages
{
    public class MeassgeProfile : Profile
    {
        public MeassgeProfile()
        {
            CreateMap<Message, IMessageDto>()
                .Include<PrivateMeassge, IPrivateMessageDto>()
                .Include<PublicMessage, IPublicMessageDto>();
            CreateMap<PrivateMeassge, IPrivateMessageDto>();
            CreateMap<PublicMessage, IPublicMessageDto>();
        }
    }
}