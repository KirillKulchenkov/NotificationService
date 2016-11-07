using System;
using PushSharp.Core;

namespace NotificationService.Domain.Logger
{
    public abstract class Logger<T> where T : INotification
    {
        protected readonly DatabaseContext _context = new DatabaseContext();
        //void NotificationStart(INotification notification);
        public abstract void NotificationFailed(T notification, Exception exp);
        public abstract void NotificationSucceed(T notification);

    }
}