using System;
using NotificationService.Domain;
using NotificationService.Domain.Logger;
using NotificationService.Domain.Models;
using PushSharp.Apple;

namespace NotificationService.Infrastructure
{
    public class IOSPushСhannel : PushСhannel<ApnsNotification>
    {
        public IOSPushСhannel(ApnsConfiguration configuration, int triesCount = 3)
            : base(new EventedConcurrentQueue<ApnsNotification>(), new ApnsServiceBroker(configuration), new AppleLogger(), triesCount)
        {
        }
        public IOSPushСhannel(EventedConcurrentQueue<ApnsNotification> incomingQueue, ApnsConfiguration configuration, int triesCount = 3) 
            : base(incomingQueue, new ApnsServiceBroker(configuration), new AppleLogger(), triesCount)
        {
        }

        protected override void BrokerInstOnOnNotificationFailed(ApnsNotification notification,
            AggregateException exception)
        {
            base.BrokerInstOnOnNotificationFailed(notification, exception);
            exception.Handle(ex =>
            {
                if (ex is ApnsNotificationException)
                {
                    int count;
                    if (WorkingQueue.TryGetValue(notification, out count) && count < TriesCount)
                    {
                        RetryQueue(notification);
                    }
                    else
                    {
                        var expiredToken = new ExpiredToken(notification.DeviceToken, null);
                        var context = new DatabaseContext();
                        context.ExpiredTokens.Add(expiredToken);
                        context.SaveChanges();
                        Logger.NotificationFailed(notification, ex);
                    }
                }
                return true;
            });

        }

       
    }
}