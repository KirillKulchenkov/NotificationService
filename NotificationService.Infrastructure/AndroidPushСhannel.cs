using System;
using System.Net.Http.Headers;
using NotificationService.Domain;
using NotificationService.Domain.Models;
using PushSharp.Core;
using PushSharp.Google;

namespace NotificationService.Infrastructure
{
    public class AndroidPushСhannel : PushСhannel<GcmNotification>
    {
        public AndroidPushСhannel(GcmConfiguration configuration, int triesCount = 3)
            : base(new EventedConcurrentQueue<GcmNotification>(), new GcmServiceBroker(configuration), new GcmLogger(), triesCount)
        {
            
        }
        public AndroidPushСhannel(EventedConcurrentQueue<GcmNotification> incomingQueue,
            GcmConfiguration configuration, int triesCount = 3) : base(incomingQueue, new GcmServiceBroker(configuration), new GcmLogger(), triesCount)
        {
            BrokerInst.OnNotificationFailed += BrokerInstOnOnNotificationFailed;
        }


        protected override void BrokerInstOnOnNotificationFailed(GcmNotification notification,
            AggregateException exception)
        {
            base.BrokerInstOnOnNotificationFailed(notification,exception);
            exception.Handle(ex =>
            {
                if (ex is GcmNotificationException)
                {
                    int count;
                    if (WorkingQueue.TryGetValue(notification, out count) && count < TriesCount)
                    {
                        RetryQueue(notification);
                    }
                    else
                    {
                        Logger.NotificationFailed(notification, exception);
                    }
                }
                else if (ex is GcmMulticastResultException)
                {
                    RemoveNotification(notification);
                    Logger.NotificationFailed(notification, exception);
                }
                else if (ex is DeviceSubscriptionExpiredException)
                {
                    //TODO: Add notification expired logic
                    var exc = ex as DeviceSubscriptionExpiredException;
                    if (exc.NewSubscriptionId != null)
                    {
                        notification.RegistrationIds.Remove(exc.OldSubscriptionId);
                        notification.RegistrationIds.Add(exc.NewSubscriptionId);
                        RetryQueue(notification);

                    }
                    else
                    {
                        Logger.NotificationFailed(notification, exception);
                    }
                    var expiredToken = new ExpiredToken(exc.OldSubscriptionId, exc.NewSubscriptionId);
                    var context = new DatabaseContext();
                    context.ExpiredTokens.Add(expiredToken);
                    context.SaveChanges();
                }
                else if (ex is RetryAfterException)
                {
                    RemoveNotification(notification);
                    Logger.NotificationFailed(notification, exception);
                }
                else
                {
                    int count;
                    if (WorkingQueue.TryGetValue(notification, out count) && count < TriesCount)
                    {
                        RetryQueue(notification);
                    }
                    else
                    {
                        Logger.NotificationFailed(notification, exception);
                    }
                }
                return true;
            });

        }
    }
}