using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NotificationService.Domain;
using NotificationService.Domain.Dto;
using NotificationService.Infrastructure;
using PushSharp.Apple;
using PushSharp.Google;

namespace NotificationService.Application
{
    public class PushService
    {
        private  Dictionary<Guid, AndroidPushСhannel> _androidPushСhannels = new Dictionary<Guid, AndroidPushСhannel>();
        private  Dictionary<Guid, IOSPushСhannel> _iosPushСhannels = new Dictionary<Guid, IOSPushСhannel>();  
        private DatabaseContext _context = new DatabaseContext();
        private int _triesCount;
        private object _updateLock = new object();
        public PushService(CoreConfig config)
        {
            _triesCount = config.TriesCount;
            UpdateApps();
        }

        public void Queue(DtoIOSNotification msg)
        {
            lock (_updateLock)
            {
                if (_iosPushСhannels.ContainsKey(msg.MobileAppId))
                {
                    // new ApnsNotification().
                    _iosPushСhannels[msg.MobileAppId].IncomingQueue.Enqueue(new ApnsNotification(msg.Token,
                        JObject.Parse(msg.Payload)));
                }
            }
        }

        public void Queue(DtoGcmNotification msg)
        {
            lock (_updateLock)
            {
                if (_androidPushСhannels.ContainsKey(msg.MobileAppId))
                {
                    _androidPushСhannels[msg.MobileAppId].IncomingQueue.Enqueue(new GcmNotification()
                    {
                        RegistrationIds = new List<string>(msg.Tokens),
                        Data = JObject.Parse(msg.Payload)
                    });
                }
            }
        }

        public void UpdateApps()
        {
            lock (_updateLock)
            {
                var mobileApps = _context.MobileAppInfoes.ToList();
                _androidPushСhannels = new Dictionary<Guid, AndroidPushСhannel>();
                _iosPushСhannels = new Dictionary<Guid, IOSPushСhannel>();
                foreach (var mobileApp in mobileApps)
                {
                    if (!_androidPushСhannels.ContainsKey(mobileApp.SourceId))
                    {
                        var ch = new AndroidPushСhannel(
                            new GcmConfiguration(mobileApp.AndroidSenderId, mobileApp.AndriodAuthtoken, ""),
                            _triesCount);
                        _androidPushСhannels.Add(mobileApp.SourceId, ch);
                        ch.Start();
                    }
                    if (!_iosPushСhannels.ContainsKey(mobileApp.SourceId))
                    {
                        var ch =
                            new IOSPushСhannel(new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production,
                                mobileApp.IOSCertificateFile, mobileApp.CertificatePassword, true));
                        _iosPushСhannels.Add(mobileApp.SourceId, ch);
                        ch.Start();
                    }
                }
            }
        }
        
    }
}
