using System;
using System.Collections.Concurrent;
using NotificationService.Domain;
using NotificationService.Domain.Logger;
using PushSharp.Core;

namespace NotificationService.Infrastructure
{
    public abstract class PushСhannel<T> where T : INotification
    {
        protected ServiceBroker<T> BrokerInst;
        public EventedConcurrentQueue<T> IncomingQueue;
        protected ConcurrentDictionary<T, int> WorkingQueue = new ConcurrentDictionary<T, int>();
        protected bool IsWorking = false;
        protected readonly object IncomingMsgLock = new object();
        protected readonly object StartLock = new object();
        protected readonly int TriesCount;
        protected Logger<T> Logger;
        protected PushСhannel(EventedConcurrentQueue<T> incomingQueue, ServiceBroker<T> broker, Logger<T> logger, int triesCount = 3)
        {
            IncomingQueue = incomingQueue;
            IncomingQueue.OnElementAdded += IncomingQueueOnOnElementAdded;
            BrokerInst = broker;
            TriesCount = triesCount;
            Logger = logger;
        }

        public void Start()
        {
            if (!IsWorking)
            {
                lock (StartLock)
                {
                    if (!IsWorking)
                    {
                        BrokerInst.Start();
                        GetMessages();
                        BrokerInst.OnNotificationSucceeded += BrokerInstOnOnNotificationSucceeded;
                        BrokerInst.OnNotificationFailed += BrokerInstOnOnNotificationFailed;
                    }
                }
            }
        }

        protected virtual void BrokerInstOnOnNotificationFailed(T notification, AggregateException exception)
        {
            int count;
            if (WorkingQueue.TryGetValue(notification, out count))
            {
                WorkingQueue.TryUpdate(notification, count + 1, count);
            }
        }
        protected virtual void BrokerInstOnOnNotificationSucceeded(T notification)
        {
            Logger.NotificationSucceed(notification);
        }
        protected void RemoveNotification(T notification)
        {
            int count;
            if (WorkingQueue.TryRemove(notification, out count))
            {

            }
        }
        protected void RetryQueue(T notification)
        {
            IncomingQueue.Enqueue(notification);
        }
        private void IncomingQueueOnOnElementAdded(object sender, QueueAddEventArgs<T> queueAddEventArgs)
        {
            if (!IsWorking)
            {
                lock (IncomingMsgLock)
                {
                    if (!IsWorking)
                    {
                        IsWorking = true;
                        GetMessages();
                    }
                }
            }
        }
        private void GetMessages()
        {
            
            IsWorking = true;
            while (IncomingQueue.Count > 0)
            {
                T msg;
                if (IncomingQueue.TryDequeue(out msg))
                {
                    if (WorkingQueue.TryAdd(msg, 0))
                    {
                        
                    }
                    BrokerInst.QueueNotification(msg);
                }
            }
            IsWorking = false;
            //BrokerInst.Stop();
        }


    }
}
