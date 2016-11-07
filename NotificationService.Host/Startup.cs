using System.Web.Http;
using System.Web.Http.Dispatcher;
using Owin;
using Unity.WebApi;

namespace NotificationService.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            //HttpListener listener =
            //    (HttpListener)appBuilder.Properties["System.Net.HttpListener"];
            //listener.AuthenticationSchemes =
            //    AuthenticationSchemes.IntegratedWindowsAuthentication;

            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new UnityDependencyResolver(UnityHelpers.GetConfiguredContainer());
            //config.Filters.Add(new AuthorizeAttribute());
            config.Services.Replace(typeof(IAssembliesResolver),
                new CustomAssemblyResolver(typeof(Application.Controllers.ApiV1.IOSPushNotificationController).Assembly));
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
            appBuilder.UseWebApi(config);
        }
    }
}