using System;
using System.Text;
using NotificationService.Domain.Models;

namespace NotificationService.Domain.Dto
{

    public class DtoMobileApp
    {
        public Guid Id { get; set; }
        public string Certificate { get; set; }
        public string CertificatePassword { get; set; }
        public string AndroidSenderId { get; set; }
        public string AuthToken { get; set; }
        public string Name { get; set; }

        public MobileAppInfo ToDataModel()
        {
            return new MobileAppInfo()
            {
                Modified = DateTime.Now,
                AndriodAuthtoken = AuthToken,
                CertificatePassword = CertificatePassword,
                IOSCertificateFile = Convert.FromBase64String(Certificate),
                MobileAppName = Name,
                SourceId = Id
            };
        }
    }
}