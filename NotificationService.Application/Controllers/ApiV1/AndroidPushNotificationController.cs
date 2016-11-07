using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NotificationService.Domain.Dto;

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
            var expiredTokens = new List<DtoExpiredToken>();
            var tokens = msg.Tokens.ToArray();
            var correctTokens = new List<string>();
            foreach (var token in tokens)
            {
                var expiredToken = Context.ExpiredTokens.FirstOrDefault(x => x.Token == token);
                if (expiredToken != null)
                {
                    expiredTokens.Add(new DtoExpiredToken()
                    {
                        Token = expiredToken.Token,
                        NewToken = expiredToken.NewToken
                    });
                }
                else
                {
                    correctTokens.Add(token);
                }
            }
            msg.Tokens = correctTokens.ToArray();
            Service.Queue(msg);
            return Ok(new DtoPushNotificationResponse()
            {
                Expired = expiredTokens
            });
        }
    }
}