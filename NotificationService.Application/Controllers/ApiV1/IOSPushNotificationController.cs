using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using NotificationService.Domain.Dto;
using NotificationService.Infrastructure;
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
            var expiredToken = TokenExperation.CheckExpired(msg.Token);
            if (expiredToken != null)
                return Ok(new DtoPushNotificationResponse()
                {
                    Expired = new[] {DtoExpiredToken.FromDataModel(expiredToken)}
                });

            Service.Queue(msg);
            return Ok(new DtoPushNotificationResponse()
            {
                Expired = null
            });
        }

        [Route("send/apple/batch")]
        [HttpPost]
        public IHttpActionResult SendPushBatch([FromBody]DtoIOSNotificationBatch msg)
        {
            var expiredTokens = TokenExperation.CheckExpired(msg.Tokens).Select(DtoExpiredToken.FromDataModel).ToArray();
            var correctTokens = new List<string>(msg.Tokens);
            foreach (var exToken in expiredTokens)
            {
                correctTokens.Remove(exToken.Token);
            }
            msg.Tokens = correctTokens.ToArray();
            foreach (var notification in msg.ToNotificationList())
            {
                Service.Queue(notification);
            }
            return Ok(new DtoPushNotificationResponse()
            {
                Expired = expiredTokens
            });
        }

    }
}