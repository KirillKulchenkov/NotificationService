using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace NotificationService.Host
{
    public class CustomAssemblyResolver : IAssembliesResolver
    {
        private readonly Assembly[] _assemblies;

        public CustomAssemblyResolver(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public ICollection<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }
}