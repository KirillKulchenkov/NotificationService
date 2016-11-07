using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Domain;
using NotificationService.Domain.Dto;
using NotificationService.Domain.Models;

namespace NotificationService.Application
{
    public class SyncMobileApps
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        public void UpdateApps(IEnumerable<DtoMobileApp> apps)
        {
            var models = apps.Select(x => x.ToDataModel());
            foreach (var mobileAppInfo in models)
            {
                TryUpdateApp(mobileAppInfo);
            }
            _context.SaveChanges();
        }

        private void TryUpdateApp(MobileAppInfo app)
        {
            var item = _context.MobileAppInfoes.FirstOrDefault(x => x.SourceId == app.SourceId);
            if (item == null)
            {
                _context.MobileAppInfoes.Add(app);
                return;
            }
            item.AndroidSenderId = app.AndroidSenderId;
            item.AndriodAuthtoken = app.AndriodAuthtoken;
            item.CertificatePassword = app.CertificatePassword;
            item.IOSCertificateFile = app.IOSCertificateFile;
            item.MobileAppName = app.MobileAppName;
            item.Modified = DateTime.Now;
        }
        
    }
}
