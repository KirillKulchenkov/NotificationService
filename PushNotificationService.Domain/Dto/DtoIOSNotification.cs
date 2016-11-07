using System;

namespace NotificationService.Domain.Dto
{
    public class DtoIOSNotification
    {
        public Guid MobileAppId { get; set; }
        public string Payload { get; set; }
        public string Token { get; set; }
        public string Alert { get; set; }
        public int Badge { get; set; }
        public string Sound { get; set; }
        public string[] Keys { get; set; }
    }
}