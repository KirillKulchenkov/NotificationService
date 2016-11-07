using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain
{
    public class EventedConcurrentQueue<T> : ConcurrentQueue<T>
    {
        public event EventHandler<QueueAddEventArgs<T>> OnElementAdded;
        public new void Enqueue(T element)
        {
            base.Enqueue(element);
            OnOnElementAdded(new QueueAddEventArgs<T>(element));
        }

        protected virtual void OnOnElementAdded(QueueAddEventArgs<T> e)
        {
            OnElementAdded?.Invoke(this, e);
        }
    }
}
