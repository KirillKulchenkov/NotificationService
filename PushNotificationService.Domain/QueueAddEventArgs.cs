using System;

namespace NotificationService.Domain
{
    public class QueueAddEventArgs<T> : EventArgs
    {
        public T Element { get; set; }

        public QueueAddEventArgs(T element)
        {
            Element = element;
        }
    }
}