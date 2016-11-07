using System;

namespace NotificationService.Domain.Dto
{
    public class DtoGcmNotification
    {
        public Guid MobileAppId { get; set; }
        public string Payload { get; set; }
        public string[] Tokens { get; set; }
    }
}