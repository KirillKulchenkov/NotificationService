using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NotificationService.Domain.Dto;
using NotificationService.Infrastructure;

namespace NotificationService.Application.Controllers.ApiV1
{
    [RoutePrefix("api/v1")]
    public class AndroidPushNotificationController : BaseController
    {
        [Route("send/gcm")]
        [HttpPost]
        public IHttpActionResult SendPush([FromBody] DtoGcmNotification msg)
        {
            //Remove expired tokens
            var expiredTokens = TokenExperation.CheckExpired(msg.Tokens.ToArray()).ToArray();
            var correctTokens = new List<string>(msg.Tokens);
            foreach (var expiredToken in expiredTokens)
            {
                correctTokens.Remove(expiredToken.Token);
            }
            msg.Tokens = correctTokens.ToArray();
            
            Service.Queue(msg);
            return Ok(new DtoPushNotificationResponse()
            {
                Expired = expiredTokens.Select(DtoExpiredToken.FromDataModel)
            });
        }
    }
}