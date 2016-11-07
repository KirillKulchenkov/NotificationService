using System;
using Microsoft.Practices.Unity;
using NotificationService.Application;
using NotificationService.Domain;

namespace NotificationService.Host
{
    public static class UnityHelpers
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType(typeof(Startup));
            
            container.RegisterInstance<PushService>(new PushService(new CoreConfig()), new ContainerControlledLifetimeManager());

        }
        
    }
}