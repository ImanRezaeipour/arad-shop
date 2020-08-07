using System;

namespace Shopping.QueryModel.QueryModels.Notifications
{
    public interface INotificationBaseDto
    {
         Guid Id { get; set; }
         DateTime CreationTime { get;  set; }
         string Title { get;  set; }
         string Body { get;  set; }
    }
}