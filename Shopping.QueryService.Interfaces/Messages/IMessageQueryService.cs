using System;
using System.Linq;
using Shopping.Infrastructure.SeedWorks;
using Shopping.QueryModel.QueryModels.Messages;

namespace Shopping.QueryService.Interfaces.Messages
{
    public interface IMessageQueryService
    {
        IQueryable<IPublicMessageDto> GetAllPublicMessage();
        IQueryable<IPrivateMessageDto> GetAllPrivateMessageByPersonId(Guid personId);
        MobilePagingResultDto<IPrivateMessageDto> GetCustomerPrivateMessageByUserId(Guid userId, PagedInputDto pagedInput);
        MobilePagingResultDto<IPrivateMessageDto> GetShopPrivateMessageByUserId(Guid userId, PagedInputDto pagedInput);
        MobilePagingResultDto<IPublicMessageDto> GetCustomerPublicMessages(PagedInputDto pagedInput);
        MobilePagingResultDto<IPublicMessageDto> GetShopPublicMessages(PagedInputDto pagedInput);
    }
}