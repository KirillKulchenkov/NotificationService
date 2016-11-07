using System.Web.Http;
using Microsoft.Practices.Unity;
using NotificationService.Domain;

namespace NotificationService.Application.Controllers
{
    public class BaseController : ApiController
    {
        public DatabaseContext Context = new DatabaseContext();

        [Dependency]
        public PushService Service { get; set; }
    }
}