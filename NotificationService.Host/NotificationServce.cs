using System;
using Microsoft.Owin.Hosting;
using NotificationService.Domain;

namespace NotificationService.Host
{
    public class NotificationServce
    {
        readonly CoreConfig _config;
        private IDisposable _service;
        public NotificationServce(CoreConfig config)
        {
            _config = config;
        }

        public void OnStart()
        {
            try
            {
                _service = WebApp.Start<Startup>(_config.HostAddress);
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        public void OnStop()
        {
            _service?.Dispose();
        }
    }
}