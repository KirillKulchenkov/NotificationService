using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationService.Domain.Dto
{
    public class DtoIOSNotificationBatch
    {
        public Guid MobileAppId { get; set; }
        public string Payload { get; set; }
        public string[] Tokens { get; set; }
        public string Alert { get; set; }
        public int Badge { get; set; }
        public string Sound { get; set; }
        public string[] Keys { get; set; }

        public IEnumerable<DtoIOSNotification> ToNotificationList()
        {
            return Tokens.Select(x => new DtoIOSNotification()
            {
                Token = x,
                Alert = Alert,
                Badge = Badge,
                Keys = Keys,
                MobileAppId = MobileAppId,
                Payload = Payload,
                Sound = Sound
            });
        }
    }
}