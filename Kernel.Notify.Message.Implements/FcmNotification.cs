using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FCM.Net;
using Kernel.Notify.Message.Interfaces;
using NLog;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Shared.Enums;

namespace Kernel.Notify.Message.Implements
{
    public class FcmNotification : IFcmNotification
    {
        private const string ShopApiServerKey = "AAAA_4Nh-BY:APA91bExsG2Zj8vGSTOScZDcrsn38Y4Hqdq0xT9HoFd4rXfHZCMXEZIZLNt_K4NyFzjnaLJR2wAHAcwcFRDQQ_oW19rfdf8GDGoJ_XRNXN6RE1VQ7N-RzVbsAQHDUVOtI_YS07gjr68I";
        //private const string ShopApiServerKey = "AAAASc8J5q0:APA91bHmQ017hJGcjvelFV9WdH6PK8uOiwOrhfTMUnlbqirw9WQS9F-eAnGGP-cXNUKftNj5-2I_z6uViNYdueADlNAS7bSYCs6vWyDe76mEPfw18_H45rEXB-ew7nHxD7mrD29V-vEbMkFW-8CrK6lEaFX3S4T5Hw";
        //private const string CustomerApiServerKey = "AAAAbZnh-P4:APA91bHNUx27PP-DlI-KFbUVgYJ-zsaovoZpAQJdJuWPpAYtFOli5JWIEFQz1XG6wPmCnoErs5Ped1F44iri4ZTzBiEoBKPb_0bVEsRrciqMy8hHIGUBQNZKqRk7rQd3vuyupQM8qrkhjLwQnQxUgfR-MqDLkKZRsA";
        private const string CustomerApiServerKey = "AAAAHWFxzoY:APA91bF56KBcd22vbEqn-v1dS9Bt1izNCRlVb-2AkQz8UJ7Lwe9tR-yGLfnV6hC3Yf2oHhu28KeOVlv1HPs6LX1BhQx7fL77jWK5Z375oMrM3Ez81n4DuXIcqfZIDTB4CUvlrIv7FWRg";
        private const string CustomerIosTopicName = "customerIOS";
        private const string CustomerTopicName = "customer";
        private const string ShopIosTopicName = "shopIOS";
        private const string ShopTopicName = "shop";
        public async Task SendToIds(List<NotifyMessage> notifyMessages, string title, string body, NotificationType notificationType, AppType appType, string sound, string additionalData = "")
        {
            using (var sender = new Sender(GetApiServerKey(appType)))
            {
                if (notifyMessages
                    .Any(item => item.OsType == OsType.Android))
                {
                    var androidMessage = new FCM.Net.Message
                    {
                        RegistrationIds = notifyMessages
                            .Where(item => item.OsType == OsType.Android)
                            .Select(item => item.RegistrationId).ToList(),
                        Data = new
                        {
                            title,
                            body,
                            notificationType,
                            additionalData
                        }
                    };
                    var androidResult = await sender.SendAsync(androidMessage);
                    if (androidResult.StatusCode != HttpStatusCode.OK || androidResult.MessageResponse.Success != 1)
                    {
                        var logger = LogManager.GetCurrentClassLogger();
                        logger.Error(androidResult);
                    }
                }
                if (notifyMessages.Any(item => item.OsType == OsType.Ios))
                {
                    var iosMessage = new FCM.Net.Message
                    {
                        RegistrationIds = notifyMessages
                            .Where(item => item.OsType == OsType.Ios)
                            .Select(item => item.RegistrationId).ToList(),
                        Data = new
                        {
                            title,
                            body,
                            notificationType,
                            additionalData
                        },
                        Notification = new Notification
                        {
                            Title = title,
                            Body = body,
                            Badge = "1",
                            Sound = sound
                        }
                    };
                    var iosResult = await sender.SendAsync(iosMessage);
                    if (iosResult.StatusCode != HttpStatusCode.OK || iosResult.MessageResponse.Success != 1)
                    {
                        var logger = LogManager.GetCurrentClassLogger();
                        logger.Error(iosResult);
                    }
                }
            }
        }
        public async Task SendToTopic(string title, string body, NotificationType notificationType, AppType appType, string sound)
        {
            using (var sender = new Sender(GetApiServerKey(appType)))
            {
                var message = new FCM.Net.Message
                {
                    To = $"/topics/{GetTopicName(appType)}",
                    Data = new
                    {
                        title,
                        body,
                        notificationType
                    }
                };
                var result = await sender.SendAsync(message);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Error(result);
                }

                var messageIos = new FCM.Net.Message
                {
                    To = $"/topics/{GetIosTopicName(appType)}",
                    Data = new
                    {
                        title,
                        body,
                        notificationType
                    },
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body,
                        Badge = "1",
                        Sound = sound
                    }
                };
                var resultIos = await sender.SendAsync(messageIos);
                if (resultIos.StatusCode != HttpStatusCode.OK)
                {
                    var logger = LogManager.GetCurrentClassLogger();
                    logger.Error(result);
                }
            }
        }
        private string GetApiServerKey(AppType appType)
        {
            switch (appType)
            {
                case AppType.Shop:
                    return ShopApiServerKey;
                case AppType.Customer:
                    return CustomerApiServerKey;
                default:
                    return "";
            }
        }
        private string GetTopicName(AppType appType)
        {
            switch (appType)
            {
                case AppType.Shop:
                    return ShopTopicName;
                case AppType.Customer:
                    return CustomerTopicName;
                default:
                    return "";
            }
        }
        private string GetIosTopicName(AppType appType)
        {
            switch (appType)
            {
                case AppType.Shop:
                    return ShopIosTopicName;
                case AppType.Customer:
                    return CustomerIosTopicName;
                default:
                    return "";
            }
        }
    }
}