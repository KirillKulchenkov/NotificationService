using System;
using System.Collections.Generic;

namespace NotificationService.Domain.Models
{
    public partial class MobileAppInfo
    {
        public MobileAppInfo()
        {
            Id = Guid.NewGuid();
            Modified = DateTime.Now;
        }
        public System.Guid Id { get; set; }
        public string MobileAppName { get; set; }
        public byte[] IOSCertificateFile { get; set; }
        public string CertificatePassword { get; set; }
        public string AndriodAuthtoken { get; set; }
        public string AndroidSenderId { get; set; }
        public System.Guid SourceId { get; set; }
        public System.DateTime Modified { get; set; }
    }
}
