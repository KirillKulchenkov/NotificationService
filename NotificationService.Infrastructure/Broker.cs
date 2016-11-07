using System;
using NotificationService.Domain.Logger;
using PushSharp;
using PushSharp.Google;
using PushSharp.Apple;
using PushSharp.Core;


namespace NotificationService.Infrastructure
{
    public abstract class Broker<T> where T : INotification
    {
        protected volatile ServiceBroker<T> BrokerInst;
        protected Logger<T> Logger;

        protected Broker(Logger<T> logger)
        {
            Logger = logger;
            BrokerInst.OnNotificationSucceeded +=BrokerInstOnOnNotificationSucceeded;
            BrokerInst.OnNotificationFailed += BrokerInstOnOnNotificationFailed;
        }

        public void QueueNotification(T notification)
        {
            BrokerInst.QueueNotification(notification);
        }

        protected abstract void BrokerInstOnOnNotificationFailed(T notification, AggregateException exception);

        protected abstract void BrokerInstOnOnNotificationSucceeded(T notification);

    }
}