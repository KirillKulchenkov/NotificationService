using System;
using NotificationService.Domain.Logger;
using NotificationService.Domain.Models;
using PushSharp.Google;

namespace NotificationService.Infrastructure
{
    public class GcmLogger : Logger<GcmNotification>
    {
        public override void NotificationFailed(GcmNotification notification, Exception exp)
        {
            _context.QueueLogs.Add(new QueueLog()
            {
                Created = DateTime.Now,
                Error = exp.Message,
                Sent = null,
                //Id = notification.MessageId,
                SourceId = null,
                TriesDone = 0,
                Payload = notification.Data.ToString(),
                Token = string.Join(", ", notification.RegistrationIds)
            });
            _context.SaveChanges();
        }

        public override void NotificationSucceed(GcmNotification notification)
        {
            _context.QueueLogs.Add(new QueueLog()
            {
                //Id = notification.MessageId,
                TriesDone = 0,
                Sent = DateTime.Now,
                Created = DateTime.Now,
                Payload = notification.Data.ToString(),
                Token = string.Join(", ", notification.RegistrationIds)
            });
            try
            {
                _context.SaveChanges();
            }
            catch (Exception exp)
            {
                
                throw;
            }
            
        }
    }
}