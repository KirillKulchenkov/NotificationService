using System.Collections.Generic;

namespace NotificationService.Domain.Dto
{
    public class DtoPushNotificationResponse
    {
        public IEnumerable<DtoExpiredToken> Expired { get; set; }
    }
}