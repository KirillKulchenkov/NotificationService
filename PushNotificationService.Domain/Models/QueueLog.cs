using System;
using System.Collections.Generic;

namespace NotificationService.Domain.Models
{
    public partial class QueueLog
    {
        public QueueLog()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }
        public Guid Id { get; set; }
        public System.DateTime Created { get; set; }
        public int TriesDone { get; set; }
        public Nullable<System.DateTime> Sent { get; set; }
        public string Error { get; set; }
        public System.Guid? SourceId { get; set; }
        public string Payload { get; set; }
        public string Token { get; set; }
    }
}
