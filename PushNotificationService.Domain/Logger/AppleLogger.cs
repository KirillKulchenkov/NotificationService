using System;
using NotificationService.Domain.Models;
using PushSharp.Apple;

namespace NotificationService.Domain.Logger
{
    public class AppleLogger : Logger<ApnsNotification>
    {
        public override void NotificationFailed(ApnsNotification notification, Exception exp)
        {
            _context.QueueLogs.Add(new QueueLog()
            {
                Created = DateTime.Now,
                Error = exp.Message,
                Sent = null,
                //Id = notification.Identifier.ToString(),
                SourceId = null,
                TriesDone = 0,
                Payload = notification.Payload.ToString(),
                Token = notification.DeviceToken
            });
            _context.SaveChanges();
        }

        public override void NotificationSucceed(ApnsNotification notification)
        {
            _context.QueueLogs.Add(new QueueLog()
            {
               // Id = notification.Identifier.ToString(),
                TriesDone = 1,
                Sent = DateTime.Now,
                Created = DateTime.Now,
                Payload = notification.Payload.ToString(),
                Token = notification.DeviceToken
            });
            _context.SaveChanges();
        }
    }
}