using System;
using System.Collections.Generic;
using System.Web.Http;
using NotificationService.Domain.Dto;

namespace NotificationService.Application.Controllers.ApiV1
{
    [RoutePrefix("api/v1")]
    public class SyncController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apps"></param>
        [Route("sync/start")]
        [HttpPost]
        public IHttpActionResult Sync([FromBody] IEnumerable<DtoMobileApp> apps)
        {
            try
            {
                var sync = new SyncMobileApps();
                sync.UpdateApps(apps);
                Service.UpdateApps();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}



