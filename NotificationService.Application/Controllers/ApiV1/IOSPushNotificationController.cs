using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using NotificationService.Domain.Dto;
using Unity.WebApi;

namespace NotificationService.Application.Controllers.ApiV1
{
    [RoutePrefix("api/v1")]
    public class IOSPushNotificationController : BaseController
    {
        
        [Route("send/apple")]
        [HttpPost]
        public IHttpActionResult SendPush([FromBody]DtoIOSNotification msg)
        {
            var expiredTokens = new List<DtoExpiredToken>();
            var expiredToken = Context.ExpiredTokens.FirstOrDefault(x => x.Token == msg.Token);
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
                Service.Queue(msg);
            }
            return Ok(new DtoPushNotificationResponse()
            {
                Expired = expiredTokens
            });
        }
    }
}