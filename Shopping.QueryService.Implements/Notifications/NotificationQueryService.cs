using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Shopping.Repository.Read.Interface;
using System.Threading.Tasks;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates;
using Shopping.DomainModel.Aggregates.Notifications.Aggregates.Abstract;
using Shopping.QueryModel.QueryModels.Notifications;
using Shopping.QueryService.Interfaces.Notifications;

namespace Shopping.QueryService.Implements.Notifications
{
    public class NotificationQueryService : INotificationQueryService
    {
        private readonly IReadOnlyRepository<NotificationBase, Guid> _repository;
        public NotificationQueryService(IReadOnlyRepository<NotificationBase, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<IList<IPanelNotificationDto>> GetUnSeenPanelNotification()
        {
            var result = await _repository.AsQuery().OfType<PanelNotification>()
                .Where(item => !item.IsVisited)
                .OrderByDescending(item => item.CreationTime).ToListAsync();
            return result.Select(item => item.ToDto()).ToList();
        }
    }
}