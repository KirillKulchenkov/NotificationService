using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using NotificationService.Application.Controllers;
using NotificationService.Domain;
using Topshelf;

namespace NotificationService.Host
{
    public static class Programm
    {
        public static void Main()
        {
            var coreConfig = new CoreConfig();
            HostFactory.Run(x =>
            {
                x.Service<NotificationServce>(s =>
                {
                    s.ConstructUsing(name => new NotificationServce(coreConfig));
                    s.WhenStarted(tc => tc.OnStart());
                    s.WhenStopped(tc => tc.OnStop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Push notification service");
                x.SetDisplayName("Notification service");
                x.SetServiceName("Notification service");
            });
            
        }
    }
}
